
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
                if (client.ReadDataFromTAA1() != -1) return client.ReadDataFromTAA1();

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
                if (client.ReadDataFromTAA3() != -1) return client.ReadDataFromTAA3();
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
                if (client.ReadDataFromTAA2() != -1) return client.ReadDataFromTAA2();
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
                if (client.ReadDataFromTAA4() != -1) return client.ReadDataFromTAA4();

                return -1;
            }
            return -1;
        }

        public void PostResetbitMachine1(int serverID)
        {
            if(serverID == 1)
            {
                client.ResetBit("TAA1");
            }else if(serverID == 2)
            {
                client.ResetBit("TAA3");
            }
        }

        public void PostResetbitMachine2(int serverID)
        {
            if (serverID == 1)
            {
                client.ResetBit("TAA2");
            }
            else if (serverID == 2)
            {
                client.ResetBit("TAA4");
            }
            throw new NotImplementedException();
        }
    }
}
