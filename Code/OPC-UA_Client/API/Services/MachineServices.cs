
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
        private static int KaputteTubenAnzTAA1;
        private static int KaputteTubenAnzTAA2;
        private static int KaputteTubenAnzTAA3;
        private static int KaputteTubenAnzTAA4;

        private static int GesamtTubenAnzBeforeTAA1 = 0;
        private static int GesamtTubenAnzBeforeTAA2 = 0;
        private static int GesamtTubenAnzBeforeTAA3 = 0;
        private static int GesamtTubenAnzBeforeTAA4 = 0;
        private static string password = null!;

        public static int gesamtTubenAnzZiel = 8000;
        private static int timeInterval = 30;

        public MachineServices()
        {

            clientServer1.EstablishConnection();
            clientServer2.EstablishConnection();

            GesamtTubenAnzBeforeTAA1 = GetGesamttubenanzahlMachine1(1);
            GesamtTubenAnzBeforeTAA2 = GetGesamttubenanzahlMachine2(1);
            GesamtTubenAnzBeforeTAA3 = GetGesamttubenanzahlMachine1(2);
            GesamtTubenAnzBeforeTAA4 = GetGesamttubenanzahlMachine2(2);
            KaputteTubenAnzTAA1 = 0;
            KaputteTubenAnzTAA2 = 0;
            KaputteTubenAnzTAA3 = 0;
            KaputteTubenAnzTAA4 = 0;
        }
        public int GetGesamttubenanzahlServer1()
        {
            Console.WriteLine("Get: GesamttubenanzahlServer1");
            return (GetGesamttubenanzahlMachine1Visual(1)) + (GetGesamttubenanzahlMachine2Visual(1));

        }

        public int GetGesamttubenanzahlServer2()
        {
            Console.WriteLine("Get: GesamttubenanzahlServer2");
            return (GetGesamttubenanzahlMachine1Visual(2)) + (GetGesamttubenanzahlMachine2Visual(2));

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

                return GetGesamttubenanzahlMachine1(serverID);

            }
            else if (serverID == 2)
            {
                if (clientServer2.ReadDataFromTAA3() != -1) return int.Parse(clientServer2.ReadDataFromTAA3().ToString()!);
                return GetGesamttubenanzahlMachine1(serverID);
            }
            return GetGesamttubenanzahlMachine1(serverID);
        }

        public int GetGesamttubenanzahlMachine2(int serverID)
        {
            Console.WriteLine("Get: GesamttubenanzahlMachine2 / server: " + serverID);

            if (serverID == 1)
            {
                if (clientServer1.ReadDataFromTAA2() != -1) return int.Parse(clientServer1.ReadDataFromTAA2().ToString()!);
                return GetGesamttubenanzahlMachine2(serverID);

            }
            else if (serverID == 2)
            {
                if (clientServer2.ReadDataFromTAA4() != -1) return int.Parse(clientServer2.ReadDataFromTAA4().ToString()!);

                return GetGesamttubenanzahlMachine2(serverID);
            }
            return GetGesamttubenanzahlMachine2(serverID);
        }
        public int GetGesamttubenanzahlMachine1Visual(int serverID)
        {
            Console.WriteLine("Get: GesamttubenanzahlMachine1 / server: " + serverID);

            if (serverID == 1)
            {

                if (clientServer1.ReadDataFromTAA1() != -1) return int.Parse(clientServer1.ReadDataFromTAA1().ToString()!) - GesamtTubenAnzBeforeTAA1;

                return GetGesamttubenanzahlMachine1Visual(serverID);

            }
            else if (serverID == 2)
            {
                if (clientServer2.ReadDataFromTAA3() != -1) return int.Parse(clientServer2.ReadDataFromTAA3().ToString()!) - GesamtTubenAnzBeforeTAA3;
                return GetGesamttubenanzahlMachine1Visual(serverID);
            }
            return -1;
        }

        public int GetGesamttubenanzahlMachine2Visual(int serverID)
        {
            Console.WriteLine("Get: GesamttubenanzahlMachine2 / server: " + serverID);

            if (serverID == 1)
            {
                if (clientServer1.ReadDataFromTAA2() != -1) return int.Parse(clientServer1.ReadDataFromTAA2().ToString()!) - GesamtTubenAnzBeforeTAA2;
                return GetGesamttubenanzahlMachine2Visual(serverID);

            }
            else if (serverID == 2)
            {
                if (clientServer2.ReadDataFromTAA4() != -1) return int.Parse(clientServer2.ReadDataFromTAA4().ToString()!) - GesamtTubenAnzBeforeTAA4;

                return GetGesamttubenanzahlMachine2Visual(serverID);
            }
            return -1;
        }

        public void PostResetbitServer1()
        {
            Console.WriteLine("POST: ResetbitServer1");
            clientServer1.ResetBitServer1();
        }

        public void PostResetbitServer2()
        {
            Console.WriteLine("POST: ResetbitServer2");
            clientServer2.ResetBitServer2();
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
        public bool PutKaputtGesamtTubenAnzTAA4(int value)
        {
            if (value <= 0) return false;
            KaputteTubenAnzTAA4 = value;
            return true;
        }

        public bool PutKaputtGesamtTubenAnzTAA3(int value)
        {
            if (value <= 0) return false;
            KaputteTubenAnzTAA3 = value;
            return true;
        }

        public bool PutKaputtGesamtTubenAnzTAA2(int value)
        {
            if (value <= 0) return false;
            KaputteTubenAnzTAA2 = value;
            return true;
        }

        public bool PutKaputtGesamtTubenAnzTAA1(int value)
        {
            if (value <= 0) return false;
            KaputteTubenAnzTAA1 = value;
            return true;
        }
    }
}
