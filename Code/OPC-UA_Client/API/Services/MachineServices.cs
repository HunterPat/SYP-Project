
using Opc.UaFx;
using Opc.UaFx.Client;
using OPC_UA_Client;
using System.Data.SQLite;

namespace API.Services
{
    public class MachineServices
    {
        public static string server1URL = "opc.tcp://localhost:4840"; //TODO: change URL
        public static string server2URL = "opc.tcp://localhost:4840"; //TODO: change URL
        MyOPCClient client;

        public MachineServices()
        {
            client = new MyOPCClient(server1URL);
            client.EstablishConnection();

        }

        public int GetGesamttubenanzahlServer1()
        {
            Console.WriteLine("Get: GesamttubenanzahlServer1");
            if (client.serverURL == server2URL)
            {
                client.Disconnect();
                client = new MyOPCClient(server1URL);
                client.EstablishConnection();
            }
            if (client.ReadDataFromTAA1() != -1 && client.ReadDataFromTAA2() != -1) return client.ReadDataFromTAA1() + client.ReadDataFromTAA1();

            return -1;

        }

        public int GetGesamttubenanzahlServer2()
        {
            Console.WriteLine("Get: GesamttubenanzahlServer2");

            if (client.serverURL == server1URL)
            {
                client.Disconnect();
                client = new MyOPCClient(server2URL);
                client.EstablishConnection();
            }
            if (client.ReadDataFromTAA3() != -1 && client.ReadDataFromTAA4() != -1) return client.ReadDataFromTAA3() + client.ReadDataFromTAA4();
            return -1;

        }
        public void GetDataFromDB()
        {
            string connectionString = "Data Source=ProdVis.sqlite;Version=3;";
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM machineData";
            var command = new SQLiteCommand(query, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["name"]);
            }
            connection.Close();
        }
        public void SaveValueIntoDB(int serverID, int gesamttubenAnz, int gesamtTubenAnzZiel, string date)
        {
            //   CreateDatabase();
            string connectionString = "Data Source=ProdVis.sqlite;Version=3;";
            SQLiteConnection connection = new SQLiteConnection(connectionString);

            connection.Open();
            string createTableSql = "CREATE TABLE IF NOT EXISTS machineData (Id INTEGER  Generated ALWAYS PRIMARY KEY , ServerID INTEGER NOT NULL, GesamttubenAnz INTEGER NOT NULL, GesamttubenAnzZiel INTEGER NOT NULL, Date TEXT NOT NULL);";
            string insertSql = "INSERT INTO machineData (ServerID, GesamttubenAnz, GesamttubenAnzZiel, Date) VALUES (@serverID, @gesamttubenAnz, @gesamtTubenAnzZiel, @date);";

            SQLiteCommand createTableCommand = new SQLiteCommand(createTableSql, connection);
            SQLiteCommand insertCommand = new SQLiteCommand(insertSql, connection);

            //Parameters
            insertCommand.Parameters.AddWithValue("@serverID", serverID);
            insertCommand.Parameters.AddWithValue("@gesamttubenAnz", gesamttubenAnz);
            insertCommand.Parameters.AddWithValue("@gesamtTubenAnzZiel", gesamtTubenAnzZiel);
            insertCommand.Parameters.AddWithValue("@date", date);


            try
            {
                createTableCommand.ExecuteNonQuery();
                insertCommand.ExecuteNonQuery();
                Console.WriteLine("Table created and data inserted!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }

        }
        public void CreateDatabase()
        {
            SQLiteConnection.CreateFile("ProdVis.sqlite");

        }

        public int GetGesamttubenanzahlMachine1(int serverID)
        {
            Console.WriteLine("Get: GesamttubenanzahlMachine1 / server" + serverID);

            if (serverID == 1)
            {
                if (client.serverURL != server1URL)
                {
                    client.Disconnect();
                    client = new MyOPCClient(server1URL);
                    client.EstablishConnection();
                }
                if (client.ReadDataFromTAA1() != -1) return int.Parse(client.ReadDataFromTAA1().ToString()!);

                return -1;

            }
            else if (serverID == 2)
            {
                if (client.serverURL != server2URL)
                {
                    client.Disconnect();
                    client = new MyOPCClient(server2URL);
                    client.EstablishConnection();
                }
                if (client.ReadDataFromTAA3() != -1) return int.Parse(client.ReadDataFromTAA3().ToString()!);
                return -1;
            }
            return -1;
        }

        public int GetGesamttubenanzahlMachine2(int serverID)
        {
            Console.WriteLine("Get: GesamttubenanzahlMachine2 / server" + serverID);

            if (serverID == 1)
            {
                if (client.serverURL != server1URL)
                {
                    client.Disconnect();
                    client = new MyOPCClient(server1URL);
                    client.EstablishConnection();
                }
                if (client.ReadDataFromTAA2() != -1) return int.Parse(client.ReadDataFromTAA2().ToString()!);
                return -1;

            }
            else if (serverID == 2)
            {
                if (client.serverURL != server2URL)
                {
                    client.Disconnect();
                    client = new MyOPCClient(server2URL);
                    client.EstablishConnection();
                }
                if (client.ReadDataFromTAA4() != -1) return int.Parse(client.ReadDataFromTAA4().ToString()!);

                return -1;
            }
            return -1;
        }

        public void PostResetbitServer1()
        {
            Console.WriteLine("POST: ResetbitServer");
            if (client.serverURL == server2URL)
            {
                client.Disconnect();
                client = new MyOPCClient(server1URL);
                client.EstablishConnection();
            }
            client.ResetBit("TAA1");
            client.ResetBit("TAA2");
        }

        public void PostResetbitServer2()
        {
            Console.WriteLine("POST: ResetbitServer2");

            if (client.serverURL == server1URL)
            {
                client.Disconnect();
                client = new MyOPCClient(server2URL);
                client.EstablishConnection();
            }
            client.ResetBit("TAA3");
            client.ResetBit("TAA4");
        }
    }
}
