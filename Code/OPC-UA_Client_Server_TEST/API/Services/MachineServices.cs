
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

        public object GetGesamttubenanzahlTAA2(int serverID)
        {
            if (serverID == 1)
            {
                if (client.serverURL != server1URL)
                {
                    client.Disconnect();
                    client = new MyOPCClient(server1URL);
                    client.EstablishConnection();
                }
                return client.ReadDataFromTAA1().Value;

            }
            else if (serverID == 2)
            {
                if (client.serverURL != server2URL)
                {
                    client.Disconnect();
                    client = new MyOPCClient(server2URL);
                    client.EstablishConnection();
                }
                return client.ReadDataFromTAA1().Value;
            }
            return null;
        }

        public object GetGesamttubenanzahlTAA1(int serverID)
        {
            if (serverID == 1)
            {
                if (client.serverURL != server1URL)
                {
                    client.Disconnect();
                    client = new MyOPCClient(server1URL);
                    client.EstablishConnection();
                }
                return client.ReadDataFromTAA2().Value;

            }
            else if (serverID == 2)
            {
                if (client.serverURL != server2URL)
                {
                    client.Disconnect();
                    client = new MyOPCClient(server2URL);
                    client.EstablishConnection();
                }
                return client.ReadDataFromTAA2().Value;
            }
            return null;
        }
    }
}
