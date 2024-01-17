using Opc.UaFx;
using Opc.UaFx.Client;
namespace OPC_UA_Client
{
    internal class MyOPCClient
    {
        OpcClient client;
        public string serverURL { get; set; }
        public MyOPCClient(string url)
        {
            client = new OpcClient(url);//OPC-Server URL
            serverURL = url;                                
        }
        public void EstablishConnection()
        {
            try
            {
                client.Connect();
                Console.WriteLine("-----------------------\nconnected!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
        public void Disconnect()
        {
            try
            {
                client.Disconnect();
                Console.WriteLine("-----------------------\ndisconnected!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }

        public OpcValue ReadDataFromTAA1()
        {
            var anzTag = "ns=4;i=6";
            var resetBitTag = "ns=4;i=7";
                var node = client.BrowseNode(OpcObjectTypes.ObjectsFolder); //refresh-Bit Tag: ns=4;i=7
               Browse(node);
            try
            {
                var gesamtAnzahl = client.ReadNode(anzTag);
                var refeshBit = client.ReadNode(resetBitTag);
                return gesamtAnzahl!;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            return null!;
        }

        public OpcValue ReadDataFromTAA2()
        {
            var anzTag = "ns=4;i=11";
            var resetBitTag = "ns=4;i=12";
            //    var node = client.BrowseNode(OpcObjectTypes.ObjectsFolder); //refresh-Bit Tag: ns=4;i=7
            //   Browse(node);
            try
            {
                var gesammtAnzahl = client.ReadNode(anzTag);
                var refeshBit = client.ReadNode(resetBitTag);
                if (gesammtAnzahl != null)
                {
                    return gesammtAnzahl;
                }
                Console.WriteLine("-----------------------\nempty Data read");
                return gesammtAnzahl!;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            return null!;
        }

        private void resetBit(string resetBitTag)
        {
            client.WriteNode(resetBitTag, true);
            Thread.Sleep(2000);//to read the Bit 
            client.WriteNode(resetBitTag, false);
        }

        public void Browse(OpcNodeInfo node, int level = 0)
        {
            Console.WriteLine("{0}{1}({2})",
                new string('.', level * 4)
                , node.Attribute(OpcAttribute.DisplayName).Value
                , node.NodeId);
            level++;

            foreach (var childNode in node.Children())
            {
                Browse(childNode, level);
            }
        }
    }
}
