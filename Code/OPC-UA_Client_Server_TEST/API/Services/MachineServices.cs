
using Opc.UaFx;
using Opc.UaFx.Client;
using OPC_UA_Client;

namespace API.Services
{
    public class MachineServices
    {
        public static string server1URL = "opc.tcp://localhost:4840/";
        public static string server2URL = "opc.tcp://localhost:4841/";
        MyOPCClient client;

        public MachineServices()
        {
            client = new MyOPCClient(server1URL);
            client.EstablishConnection();

        }

        public long GetGesamttubenanzahlServer1()
        {
            if (client.serverURL == server2URL)
            {
                client.Disconnect();
                client = new MyOPCClient(server1URL);
                client.EstablishConnection();
            }
            if (client.ReadDataFromTAA1() != -1 && client.ReadDataFromTAA2() != -1) return client.ReadDataFromTAA1() + client.ReadDataFromTAA1();

            return -1;

        }

        public long GetGesamttubenanzahlServer2()
        {

            if (client.serverURL == server1URL)
            {
                client.Disconnect();
                client = new MyOPCClient(server2URL);
                client.EstablishConnection();
            }
            if (client.ReadDataFromTAA3() != -1 && client.ReadDataFromTAA4() != -1) return client.ReadDataFromTAA3() + client.ReadDataFromTAA4();
            return -1;

        }
        public long GetGesamttubenanzahlMachine1(int serverID)
        {
            if (serverID == 1)
            {
                if (client.serverURL != server1URL)
                {
                    client.Disconnect();
                    client = new MyOPCClient(server1URL);
                    client.EstablishConnection();
                }
                if (client.ReadDataFromTAA1() != -1) return long.Parse(client.ReadDataFromTAA1().ToString()!);

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
                if (client.ReadDataFromTAA3() != -1) return long.Parse(client.ReadDataFromTAA3().ToString()!);
                return -1;
            }
            return -1;
        }

        public long GetGesamttubenanzahlMachine2(int serverID)
        {
            if (serverID == 1)
            {
                if (client.serverURL != server1URL)
                {
                    client.Disconnect();
                    client = new MyOPCClient(server1URL);
                    client.EstablishConnection();
                }
                if (client.ReadDataFromTAA2() != -1) return long.Parse(client.ReadDataFromTAA2().ToString()!);
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
                if (client.ReadDataFromTAA4() != -1) return long.Parse(client.ReadDataFromTAA4().ToString()!);

                return -1;
            }
            return -1;
        }

        public void PostResetbitServer1()
        {
            if (client.serverURL == server2URL)
            {
                client.Disconnect();
                client = new MyOPCClient(server1URL);
                client.EstablishConnection();
            }
            client.ResetBit("TAA1");
            client.ResetBit("TAA2");
        }

        public void PostResetbitMachine2()
        {
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
