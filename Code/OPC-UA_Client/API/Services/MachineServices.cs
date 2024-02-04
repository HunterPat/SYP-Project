
using Opc.UaFx;
using Opc.UaFx.Client;
using OPC_UA_Client;

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
                if (client.ReadDataFromTAA1() != null) return long.Parse(client.ReadDataFromTAA1().Value.ToString()!);

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
                if (client.ReadDataFromTAA3() != null) return long.Parse(client.ReadDataFromTAA3().Value.ToString()!);
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
                if (client.ReadDataFromTAA2() != null) return long.Parse(client.ReadDataFromTAA2().Value.ToString()!);
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
                if (client.ReadDataFromTAA4() != null) return long.Parse(client.ReadDataFromTAA4().Value.ToString()!);

                return -1;
            }
            return -1;
        }

        public void PostResetBitMachine1(int serverID)
        {
            if (serverID == 1)
            {
                client.ResetBit("ns=4;i=7");
            }
            else if (serverID == 2)
            {
                client.ResetBit("");//TODO: insert value
            }
        }

        public void PostResetBitMachine2(int serverID)
        {
            if (serverID == 1)
            {
                client.ResetBit("ns=4;i=12");

            }
            else if (serverID == 2)
            {
                client.ResetBit(""); //TODO: insert value

            }
        }
    }
}
