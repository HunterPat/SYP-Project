
using Opc.UaFx;
using Opc.UaFx.Client;
using OPC_UA_Client;
using System.Data.SQLite;

namespace API.Services
{
    public class MachineServices
    {
        public static string server1URL = "opc.tcp://192.168.1.10:4840"; //TODO: change URL
        public static string server2URL = "opc.tcp://192.168.1.20:4850"; //TODO: change URL
        static MyOPCClient clientServer1 = new MyOPCClient(server1URL);
        static MyOPCClient clientServer2 = new MyOPCClient(server2URL);

        public MachineServices()
        {

            clientServer1.EstablishConnection();
            clientServer2.EstablishConnection();

        }
        public int GetGesamttubenanzahlServer1()
        {
            Console.WriteLine("Get: GesamttubenanzahlServer1");
            if (clientServer1.ReadDataFromTAA1() != -1 && clientServer1.ReadDataFromTAA2() != -1) return clientServer1.ReadDataFromTAA1() + clientServer1.ReadDataFromTAA2();

            return -1;

        }

        public int GetGesamttubenanzahlServer2()
        {
            Console.WriteLine("Get: GesamttubenanzahlServer2");

            if (clientServer2.ReadDataFromTAA3() != -1 && clientServer2.ReadDataFromTAA4() != -1) return clientServer2.ReadDataFromTAA3() + clientServer2.ReadDataFromTAA4();
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
            Console.WriteLine("Get: GesamttubenanzahlMachine1 / server: " + serverID);

            if (serverID == 1)
            {

                if (clientServer1.ReadDataFromTAA1() != -1) return int.Parse(clientServer1.ReadDataFromTAA1().ToString()!);

                return -1;

            }
            else if (serverID == 2)
            {
                if (clientServer2.ReadDataFromTAA3() != -1) return int.Parse(clientServer2.ReadDataFromTAA3().ToString()!);
                return -1;
            }
            return -1;
        }

        public int GetGesamttubenanzahlMachine2(int serverID)
        {
            Console.WriteLine("Get: GesamttubenanzahlMachine2 / server: " + serverID);

            if (serverID == 1)
            {
                if (clientServer1.ReadDataFromTAA2() != -1) return int.Parse(clientServer1.ReadDataFromTAA2().ToString()!);
                return -1;

            }
            else if (serverID == 2)
            {
                if (clientServer2.ReadDataFromTAA4() != -1) return int.Parse(clientServer2.ReadDataFromTAA4().ToString()!);

                return -1;
            }
            return -1;
        }

        public void PostResetbitServer1()
        {
            Console.WriteLine("POST: ResetbitServer");

            clientServer1.ResetBit();
            clientServer1.ResetBit();
        }

        public void PostResetbitServer2()
        {
            Console.WriteLine("POST: ResetbitServer2");
            clientServer2.ResetBit();
            clientServer2.ResetBit();
        }

        private int CalculatePercentage(int numerator, int denominator)
        {
            if (denominator != 0)
            {
                double result = ((double)numerator / denominator) * 100;
                return (int)result;
            }
            return 0;
        }

        public int GetGesamttubenanzahlPercentServer2(int gesamttubenAnzZiel)
        {
            return CalculatePercentage(GetGesamttubenanzahlServer2(), GetGesamttubenanzahlZielMachinePairs(gesamttubenAnzZiel));
        }

        public int GetGesamttubenanzahlPercentServer1(int gesamttubenAnzZiel)
        {
            return CalculatePercentage(GetGesamttubenanzahlServer1(), GetGesamttubenanzahlZielMachinePairs(gesamttubenAnzZiel));
        }
        public int GetGesamttubenanzahlPercentTAA1(int gesamttubenAnzZiel)
        {
            return CalculatePercentage(GetGesamttubenanzahlMachine1(1), GetGesamttubenanzahlZiel4Machines(gesamttubenAnzZiel));
        }
        public int GetGesamttubenanzahlPercentTAA2(int gesamttubenAnzZiel)
        {
            return CalculatePercentage(GetGesamttubenanzahlMachine2(1), GetGesamttubenanzahlZiel4Machines(gesamttubenAnzZiel));
        }
        public int GetGesamttubenanzahlPercentTAA3(int gesamttubenAnzZiel)
        {
            return CalculatePercentage(GetGesamttubenanzahlMachine1(2), GetGesamttubenanzahlZiel4Machines(gesamttubenAnzZiel));
        }
        public int GetGesamttubenanzahlPercentTAA4(int gesamttubenAnzZiel)
        {
            return CalculatePercentage(GetGesamttubenanzahlMachine2(2), GetGesamttubenanzahlZiel4Machines(gesamttubenAnzZiel));
        }
        public int GetGesamttubenanzahlZielMachinePairs(int gesamtTubenAnzZiel)
        {
            return gesamtTubenAnzZiel / 2;
        }
        public int GetGesamttubenanzahlZiel4Machines(int gesamtTubenAnzZiel)
        {
            return gesamtTubenAnzZiel / 4;
        }
    }
}
