
using System.Security.Principal;
using API.Maps;
using Eco.FrameworkImpl.Ocl;
using Opc.Ua;
using Opc.UaFx;
using Opc.UaFx.Client;
using OPC_UA_Client;

namespace API.Services
{
    public class MachineServices
    {
        public static string server1URL = "opc.tcp://localhost:4840/";
        public static string server2URL = "opc.tcp://localhost:4841/";
        static MyOPCClient clientServer1 = new MyOPCClient(server1URL);
        static MyOPCClient clientServer2 = new MyOPCClient(server2URL);
        private static int KaputteTubenAnzTAA1 = 0;
        private static int KaputteTubenAnzTAA2 = 0;
        private static int KaputteTubenAnzTAA3 = 0;
        private static int KaputteTubenAnzTAA4 = 0;

        private static int GesamtTubenAnzBeforeTAA1 = 0;
        private static int GesamtTubenAnzBeforeTAA2 = 0;
        private static int GesamtTubenAnzBeforeTAA3 = 0;
        private static int GesamtTubenAnzBeforeTAA4 = 0;
        private static string password = "dog";

        public static int gesamtTubenAnzZiel = 800;
        private static int timeInterval = 30;
        private static string finalCSVPath = null!;

        public MachineServices()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            for (int i = 0; i < 5; i++)
            {
                currentDirectory = Directory.GetParent(currentDirectory)!.FullName;
            }
            // Append the remaining path
            finalCSVPath = currentDirectory + @"\OPC-UA_Client_Server_TEST\API\";
            clientServer1.EstablishConnection();
            clientServer2.EstablishConnection();
            ReadLatestValuesFromProdVis();
        }
        public void ResetAllValuesAndCSV()
        {
            ClearProdVis();
            GesamtTubenAnzBeforeTAA1 = GetGesamttubenanzahlMachine1(1);
            GesamtTubenAnzBeforeTAA2 = GetGesamttubenanzahlMachine2(1);
            GesamtTubenAnzBeforeTAA3 = GetGesamttubenanzahlMachine1(2);
            GesamtTubenAnzBeforeTAA4 = GetGesamttubenanzahlMachine2(2);
            KaputteTubenAnzTAA1 = 0;
            KaputteTubenAnzTAA2 = 0;
            KaputteTubenAnzTAA3 = 0;
            KaputteTubenAnzTAA4 = 0;
        }


        private static void ClearProdVis()
        {
            using (StreamWriter writer = new StreamWriter(finalCSVPath + "ProdVis.csv", false))
            {
                writer.Write("");
            }
            Console.WriteLine("CSV file cleared successfully.");
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
        public void ReadLatestValuesFromProdVis()
        {
            var taa1Val = 0;
            var taa2Val = 0;
            var taa3Val = 0;
            var taa4Val = 0;
            using (StreamReader reader = new StreamReader(finalCSVPath + "ProdVis.csv"))
            {
                var readLine = reader.ReadLine();
                while (readLine != null)
                {
                    if (readLine.Length <= 0)
                    {
                        readLine = reader.ReadLine();
                    }
                    else
                    {
                        var splittedLine = readLine.Split(";");
                        if (int.Parse(splittedLine[0]) == 1)
                        {
                            taa1Val = int.Parse(splittedLine[1]);
                            GesamtTubenAnzBeforeTAA1 = int.Parse(splittedLine[2]);
                            KaputteTubenAnzTAA1 = int.Parse(splittedLine[3]);
                        }
                        else if (int.Parse(splittedLine[0]) == 2)
                        {
                            taa2Val = int.Parse(splittedLine[1]);
                            GesamtTubenAnzBeforeTAA2 = int.Parse(splittedLine[2]);

                            KaputteTubenAnzTAA2 = int.Parse(splittedLine[3]);
                        }
                        else if (int.Parse(splittedLine[0]) == 3)
                        {
                            taa3Val = int.Parse(splittedLine[1]);
                            GesamtTubenAnzBeforeTAA3 = int.Parse(splittedLine[2]);

                            KaputteTubenAnzTAA3 = int.Parse(splittedLine[3]);
                        }
                        else if (int.Parse(splittedLine[0]) == 4)
                        {
                            taa4Val = int.Parse(splittedLine[1]);
                            GesamtTubenAnzBeforeTAA4 = int.Parse(splittedLine[2]);
                            KaputteTubenAnzTAA4 = int.Parse(splittedLine[3]);
                        }
                        gesamtTubenAnzZiel = int.Parse(splittedLine[4]);
                        timeInterval = int.Parse(splittedLine[5]);
                        readLine = reader.ReadLine();
                    }
                }
            }

            MachineMaps.server1.InitServerValues(taa1Val, taa2Val);
            MachineMaps.server2.InitServer2Values(taa3Val, taa4Val);
        }

        public void SaveValueIntoProdVisLongTerm()
        {
            using (StreamWriter writer = new StreamWriter(finalCSVPath + "ProdVisLongTerm.csv", true))
            {
                for (int i = 1; i < 5; i++)
                {
                    var writeLine = "";
                    writeLine += i + ";";
                    if (i == 1)
                    {

                        writeLine += GetGesamttubenanzahlMachine1Visual(1) + ";";
                        writeLine += KaputteTubenAnzTAA1 + ";";
                    }
                    else if (i == 2)
                    {
                        writeLine += GetGesamttubenanzahlMachine2Visual(1) + ";";
                        writeLine += KaputteTubenAnzTAA2 + ";";
                    }
                    else if (i == 3)
                    {
                        writeLine += GetGesamttubenanzahlMachine1Visual(2) + ";";
                        writeLine += KaputteTubenAnzTAA3 + ";";
                    }
                    else if (i == 4)
                    {
                        writeLine += GetGesamttubenanzahlMachine2Visual(2) + ";";
                        writeLine += KaputteTubenAnzTAA4 + ";";
                    }

                    writeLine += GetGesamttubenanzahlZiel4Machines() + ";";
                    writeLine += DateTime.Now.ToString("dd.MM.yyyy") + ";";
                    writer.WriteLine(writeLine);
                    writer.Flush();
                    Console.WriteLine("Saved into CSV");
                }
                //writer.Close();
            }
        }

        public Dictionary<int, List<string>> ReadValuesOfLongTermCSV(DateTime dateTime)
        {
            Dictionary<int, List<string>> machineDictionary = new Dictionary<int, List<string>>();

            using (StreamReader reader = new StreamReader(finalCSVPath + "ProdVisLongTerm.csv"))
            {
                var readLine = reader.ReadLine();
                while (readLine != null)
                {
                    if (readLine.Length <= 0)
                    {
                        readLine = reader.ReadLine();
                    }
                    else
                    {
                        if (readLine.Contains(dateTime.ToString("dd.MM.yyyy")))
                        {
                            var splittedLine = readLine.Split(";");
                            for (int i = 1; i < 5; i++)
                            {
                                if (readLine != null)
                                {
                                    if (readLine.Contains(dateTime.ToString("dd.MM.yyyy")))
                                    {
                                        machineDictionary.Add(int.Parse(splittedLine[0]), new List<string> { splittedLine[1], splittedLine[2], splittedLine[3], splittedLine[4] });
                                        readLine = reader.ReadLine();
                                    }

                                }
                            }
                            break;
                        }
                    }
                }
            }
            return machineDictionary;
        }
        public void SaveCurrentValuesIntoProdVis()
        {
            using (StreamWriter writer = new StreamWriter(finalCSVPath + "ProdVis.csv", true))
            {
                for (int i = 1; i < 5; i++)
                {
                    var writeLine = "";
                    writeLine += i + ";";
                    if (i == 1)
                    {

                        writeLine += GetGesamttubenanzahlMachine1(1) + ";";
                        writeLine += GesamtTubenAnzBeforeTAA1 + ";";

                        writeLine += KaputteTubenAnzTAA1 + ";";
                    }
                    else if (i == 2)
                    {
                        writeLine += GetGesamttubenanzahlMachine2(1) + ";";
                        writeLine += GesamtTubenAnzBeforeTAA2 + ";";

                        writeLine += KaputteTubenAnzTAA2 + ";";
                    }
                    else if (i == 3)
                    {
                        writeLine += GetGesamttubenanzahlMachine1(2) + ";";
                        writeLine += GesamtTubenAnzBeforeTAA3 + ";";

                        writeLine += KaputteTubenAnzTAA3 + ";";
                    }
                    else if (i == 4)
                    {
                        writeLine += GetGesamttubenanzahlMachine2(2) + ";";
                        writeLine += GesamtTubenAnzBeforeTAA4 + ";";

                        writeLine += KaputteTubenAnzTAA4 + ";";
                    }

                    writeLine += gesamtTubenAnzZiel + ";";
                    writeLine += timeInterval + ";";
                    writer.WriteLine(writeLine);
                    writer.Flush();
                    Console.WriteLine("Saved into CSV");
                }
            }
        }

        /*   public static void SaveCurrentValuesIntoDB()
       {
           string connectionString = "Data Source=ProdVis.sqlite;Version=3;";
           SQLiteConnection connection = new SQLiteConnection(connectionString);
           Console.Write(connection.State);
           connection.Open();
           Console.Write(connection.State);

           string createTableSql = "CREATE TABLE IF NOT EXISTS machineData (Id INTEGER  Generated ALWAYS PRIMARY KEY , MachineId INTEGER NOT NULL, GesamttubenAnz INTEGER NOT NULL, KaputteTubenAnz INTEGER NOT NULL, TimeInterval INTEGER, GesamttubenAnzZiel INTEGER NOT NULL, Date TEXT NOT NULL);";
           string insertSql = "INSERT INTO machineData (MachineId, GesamttubenAnz, KaputteTubenAnz, TimeInterval, GesamttubenAnzZiel, Date) VALUES (@MachineId, @GesamttubenAnz, @KaputteTubenAnz, @TimeInterval, @GesamttubenAnzZiel, @Date);";

           try
           {
               SQLiteCommand createTableCommand = new SQLiteCommand(createTableSql, connection);
               createTableCommand.ExecuteNonQuery();

               for (int i = 1; i < 5; i++)
               {
                   SQLiteCommand insertCommand = new SQLiteCommand(insertSql, connection);

                   // Parameters
                   insertCommand.Parameters.AddWithValue("@MachineId", i);
                   if (i == 1)
                   {
                       insertCommand.Parameters.AddWithValue("@GesamttubenAnz", GesamtTubenAnzBeforeTAA1);
                       insertCommand.Parameters.AddWithValue("@KaputteTubenAnz", KaputteTubenAnzTAA1);
                   }
                   else if (i == 2)
                   {
                       insertCommand.Parameters.AddWithValue("@GesamttubenAnz", GesamtTubenAnzBeforeTAA2);
                       insertCommand.Parameters.AddWithValue("@KaputteTubenAnz", KaputteTubenAnzTAA2);
                   }
                   else if (i == 3)
                   {
                       insertCommand.Parameters.AddWithValue("@GesamttubenAnz", GesamtTubenAnzBeforeTAA3);
                       insertCommand.Parameters.AddWithValue("@KaputteTubenAnz", KaputteTubenAnzTAA3);
                   }
                   else if (i == 4)
                   {
                       insertCommand.Parameters.AddWithValue("@GesamttubenAnz", GesamtTubenAnzBeforeTAA4);
                       insertCommand.Parameters.AddWithValue("@KaputteTubenAnz", KaputteTubenAnzTAA4);
                   }
                   insertCommand.Parameters.AddWithValue("@GesamttubenAnzZiel", gesamtTubenAnzZiel);
                   insertCommand.Parameters.AddWithValue("@TimeInterval", timeInterval);
                   insertCommand.Parameters.AddWithValue("@Date", DateTime.Now.ToString("dd.MMMM.yyyy hh:mm"));

                   insertCommand.ExecuteNonQuery();
               }

               Console.WriteLine("Data saved into DB");
           }
           catch (Exception ex)
           {
               Console.WriteLine($"Error: {ex.Message}");
           }
           finally
           {
               connection.Close();
           }

       }*/
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
            return -1;
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
            return -1;
        }

        public int GetGesamttubenanzahlMachine1Visual(int serverID)
        {
            Console.WriteLine("Get: GesamttubenanzahlMachine1 / server: " + serverID);

            if (serverID == 1)
            {

                if (clientServer1.ReadDataFromTAA1() != -1) return clientServer1.ReadDataFromTAA1() - GesamtTubenAnzBeforeTAA1;

                return GetGesamttubenanzahlMachine1Visual(serverID);

            }
            else if (serverID == 2)
            {
                if (clientServer2.ReadDataFromTAA3() != -1) return clientServer2.ReadDataFromTAA3() - GesamtTubenAnzBeforeTAA3;
                return GetGesamttubenanzahlMachine1Visual(serverID);
            }
            return -1;
        }

        public int GetGesamttubenanzahlMachine2Visual(int serverID)
        {
            Console.WriteLine("Get: GesamttubenanzahlMachine2 / server: " + serverID);

            if (serverID == 1)
            {
                if (clientServer1.ReadDataFromTAA2() != -1) return clientServer1.ReadDataFromTAA2() - GesamtTubenAnzBeforeTAA2;
                return GetGesamttubenanzahlMachine2Visual(serverID);

            }
            else if (serverID == 2)
            {
                if (clientServer2.ReadDataFromTAA4() != -1) return clientServer2.ReadDataFromTAA4() - GesamtTubenAnzBeforeTAA4;

                return GetGesamttubenanzahlMachine2Visual(serverID);
            }
            return -1;
        }

        public void PostResetbitServer1()
        {
            Console.WriteLine("POST: ResetbitServer");

            clientServer1.ResetBit("TAA1");
            clientServer1.ResetBit("TAA2");
            GesamtTubenAnzBeforeTAA1 = 0;
            GesamtTubenAnzBeforeTAA2 = 0;
        }

        public void PostResetbitServer2()
        {
            Console.WriteLine("POST: ResetbitServer2");
            clientServer2.ResetBit("TAA3");
            clientServer2.ResetBit("TAA4");
            GesamtTubenAnzBeforeTAA3 = 0;
            GesamtTubenAnzBeforeTAA4 = 0;
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
            return CalculatePercentage(GetGesamttubenanzahlMachine1Visual(1), GetGesamttubenanzahlZiel4Machines());
        }
        public int GetGesamttubenanzahlPercentTAA2()
        {
            return CalculatePercentage(GetGesamttubenanzahlMachine2Visual(1), GetGesamttubenanzahlZiel4Machines());
        }
        public int GetGesamttubenanzahlPercentTAA3()
        {
            return CalculatePercentage(GetGesamttubenanzahlMachine1Visual(2), GetGesamttubenanzahlZiel4Machines());
        }
        public int GetGesamttubenanzahlPercentTAA4()
        {
            return CalculatePercentage(GetGesamttubenanzahlMachine2Visual(2), GetGesamttubenanzahlZiel4Machines());
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
            gesamtTubenAnzZiel = value <= 0 || value % 2 != 0 ? gesamtTubenAnzZiel : value;
            return gesamtTubenAnzZiel == value;
        }

        public int GetTimeInterval()
        {
            return timeInterval;
        }
        public bool PutKaputtGesamtTubenAnzTAA4(int value)
        {
            if (value < 0) return false;
            KaputteTubenAnzTAA4 = value;
            return true;
        }

        public bool PutKaputtGesamtTubenAnzTAA3(int value)
        {
            if (value < 0) return false;
            KaputteTubenAnzTAA3 = value;
            return true;
        }

        public bool PutKaputtGesamtTubenAnzTAA2(int value)
        {
            if (value < 0) return false;
            KaputteTubenAnzTAA2 = value;
            return true;
        }

        public bool PutKaputtGesamtTubenAnzTAA1(int value)
        {
            if (value < 0) return false;
            KaputteTubenAnzTAA1 = value;
            return true;
        }
    }
}
