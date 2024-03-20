
using Opc.Ua;
using Opc.UaFx;
using Opc.UaFx.Client;
using OPC_UA_Client;
using Org.BouncyCastle.Asn1.Mozilla;
using System.Net.Sockets;

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

        private static int GesamtTubenAnzBeforeTAA1 = 0;
        private static int GesamtTubenAnzBeforeTAA2 = 0;
        private static int GesamtTubenAnzBeforeTAA3 = 0;
        private static int GesamtTubenAnzBeforeTAA4 = 0;
        private static string password = null!;
        private string finalCSVPath;
        public static int gesamtTubenAnzZiel = 8000;
        private static int timeInterval = 30;

        public MachineServices()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            for (int i = 0; i < 5; i++)
            {
                currentDirectory = Directory.GetParent(currentDirectory).FullName;
            }
            // Append the remaining path
            finalCSVPath = currentDirectory + @"\OPC-UA_Client\API\ProdVis.csv";
            ReadLatestValuesFromCSV();
            clientServer1.EstablishConnection();
            clientServer2.EstablishConnection();
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
        public void ReadLatestValuesFromCSV()
        {
            using (StreamReader reader = new StreamReader(finalCSVPath))
            {
                var readLine = reader.ReadLine();
                while (readLine != null || readLine!.Length > 0)
                {
                    var splittedLine = readLine.Split(";");
                    if (int.Parse(splittedLine[0]) == 1)
                    {
                        GesamtTubenAnzBeforeTAA1 = int.Parse(splittedLine[1]);
                        KaputteTubenAnzTAA1 = int.Parse(splittedLine[2]);
                    }
                    else if (int.Parse(splittedLine[0]) == 2)
                    {
                        GesamtTubenAnzBeforeTAA3 = int.Parse(splittedLine[1]);
                        KaputteTubenAnzTAA3 = int.Parse(splittedLine[2]);
                    }
                    else if (int.Parse(splittedLine[0]) == 3)
                    {
                        GesamtTubenAnzBeforeTAA3 = int.Parse(splittedLine[1]);
                        KaputteTubenAnzTAA3 = int.Parse(splittedLine[2]);
                    }
                    else if (int.Parse(splittedLine[0]) == 4)
                    {
                        GesamtTubenAnzBeforeTAA4 = int.Parse(splittedLine[1]);
                        KaputteTubenAnzTAA4 = int.Parse(splittedLine[2]);
                    }
                    gesamtTubenAnzZiel = int.Parse(splittedLine[3]);
                    timeInterval = int.Parse(splittedLine[4]);
                }
            }
        }
        public void ResetAllValuesAndCSV()
        {
            using (StreamWriter writer = new StreamWriter(finalCSVPath, false))
            {
                writer.Write(""); // Write an empty string to clear the file
            }
            Console.WriteLine("CSV file cleared successfully.");
            GesamtTubenAnzBeforeTAA1 = GetGesamttubenanzahlMachine1(1);
            GesamtTubenAnzBeforeTAA2 = GetGesamttubenanzahlMachine2(1);
            GesamtTubenAnzBeforeTAA3 = GetGesamttubenanzahlMachine1(2);
            GesamtTubenAnzBeforeTAA4 = GetGesamttubenanzahlMachine2(2);
            KaputteTubenAnzTAA1 = 0;
            KaputteTubenAnzTAA2 = 0;
            KaputteTubenAnzTAA3 = 0;
            KaputteTubenAnzTAA4 = 0;
        }

        public void SaveCurrentValuesIntoDB()
        {

            using (StreamWriter writer = new StreamWriter(finalCSVPath, true))
            {
                for (int i = 1; i < 5; i++)
                {
                    var writeLine = "";
                    writeLine += i + ";";
                    if (i == 1)
                    {
                        writeLine += GesamtTubenAnzBeforeTAA1 + ";";
                        writeLine += KaputteTubenAnzTAA1 + ";";
                    }
                    else if (i == 2)
                    {
                        writeLine += GesamtTubenAnzBeforeTAA2 + ";";
                        writeLine += KaputteTubenAnzTAA2 + ";";
                    }
                    else if (i == 3)
                    {
                        writeLine += GesamtTubenAnzBeforeTAA3 + ";";
                        writeLine += KaputteTubenAnzTAA3 + ";";
                    }
                    else if (i == 4)
                    {
                        writeLine += GesamtTubenAnzBeforeTAA4 + ";";
                        writeLine += KaputteTubenAnzTAA4 + ";";
                    }

                    writeLine += gesamtTubenAnzZiel + ";";
                    writeLine += timeInterval + ";";
                    writer.WriteLine(writeLine);
                    writer.Flush();
                    Console.WriteLine("Saved into CSV");
                }
                writer.Close();
            }
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
