
using Opc.UaFx;
using Opc.UaFx.Client;
using OPC_UA_Client;
using Org.BouncyCastle.Asn1.Mozilla;
using System.Data.SQLite;

namespace API.Services
{
    public class MachineServices
    {
        public static string server1URL = "opc.tcp://192.168.1.10:4840"; //TODO: change URL
        public static string server2URL = "opc.tcp://192.168.1.20:4850"; //TODO: change URL
        static MyOPCClient clientServer1 = new MyOPCClient(server1URL);
        static MyOPCClient clientServer2 = new MyOPCClient(server2URL);
        private static int KaputteTubenAnzTAA1 = 0;
        private static int KaputteTubenAnzTAA2 = 0;
        private static int KaputteTubenAnzTAA3 = 0;
        private static int KaputteTubenAnzTAA4 = 0;
        private static string password = null!;

        public static int gesamtTubenAnzZiel = 8000;
        private static int timeInterval = 30;

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

        public int GetGesamttubenanzahlPercentServer2()
        {
            return CalculatePercentage(GetGesamttubenanzahlServer2(), GetGesamttubenanzahlZielMachinePairs());
        }

        public int GetGesamttubenanzahlPercentServer1()
        {
            return CalculatePercentage(GetGesamttubenanzahlServer1(), GetGesamttubenanzahlZielMachinePairs());
        }
        public int GetGesamttubenanzahlPercentTAA1()
        {
            return CalculatePercentage(GetGesamttubenanzahlMachine1(1), GetGesamttubenanzahlZiel4Machines());
        }
        public int GetGesamttubenanzahlPercentTAA2()
        {
            return CalculatePercentage(GetGesamttubenanzahlMachine2(1), GetGesamttubenanzahlZiel4Machines());
        }
        public int GetGesamttubenanzahlPercentTAA3()
        {
            return CalculatePercentage(GetGesamttubenanzahlMachine1(2), GetGesamttubenanzahlZiel4Machines());
        }
        public int GetGesamttubenanzahlPercentTAA4()
        {
            return CalculatePercentage(GetGesamttubenanzahlMachine2(2), GetGesamttubenanzahlZiel4Machines());
        }
        public int GetGesamttubenanzahlZielMachinePairs()
        {
            return gesamtTubenAnzZiel / 2;
        }
        public int GetGesamttubenanzahlZiel4Machines()
        {
            return gesamtTubenAnzZiel / 4;
        }

        public void PostKaputtGesamtTubenAnzTAA1(int value)
        {

            KaputteTubenAnzTAA1 = value > 0 ? value : KaputteTubenAnzTAA1;
        }

        public void PostKaputtGesamtTubenAnzTAA2(int value)
        {
            KaputteTubenAnzTAA2 = value > 0 ? value : KaputteTubenAnzTAA2;

        }

        public void PostKaputtGesamtTubenAnzTAA3(int value)
        {
            KaputteTubenAnzTAA3 = value > 0 ? value : KaputteTubenAnzTAA3;

        }

        public void PostKaputtGesamtTubenAnzTAA4(int value)
        {
            KaputteTubenAnzTAA4 = value > 0 ? value : KaputteTubenAnzTAA4;

        }

        public int GetKaputtGesamtTubenAnzTAA4()
        {
            return KaputteTubenAnzTAA4;
        }

        public int GetKaputtGesamtTubenAnzTAA3()
        {
            return KaputteTubenAnzTAA3;
        }

        public int GetKaputtGesamtTubenAnzTAA2()
        {
            return KaputteTubenAnzTAA2;
        }

        public int GetKaputtGesamtTubenAnzTAA1()
        {
            return KaputteTubenAnzTAA1;
        }

        public bool CheckPassword(string passwordValue)
        {
            return password == passwordValue;
        }

        public bool PutNewPassword(string newPasswordValue)
        {
            password = newPasswordValue.Length > 0 && newPasswordValue != null ? newPasswordValue : password;
            return password == newPasswordValue;
        }
        public string GetPassword()
        {
            return password;
        }

        public bool PutTimeInterval(int intervalValue)
        {
            timeInterval = intervalValue > 0 ? intervalValue : timeInterval;
            return timeInterval == intervalValue;
        }

        public int GetGesamttubenanzahlZiel()
        {
            return gesamtTubenAnzZiel;
        }

        public bool PutGesamttubenAnzZiel(int value)
        {
            gesamtTubenAnzZiel = value > 0 ? gesamtTubenAnzZiel : value;
            return gesamtTubenAnzZiel == value;
        }

        public int GetTimeInterval()
        {
            return timeInterval;
        }
    }
}
