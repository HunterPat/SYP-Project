using Opc.Ua;
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
                //    var node = client.BrowseNode(OpcObjectTypes.ObjectsFolder); //refresh-Bit Tag: ns=4;i=7
                //  Browse(node);
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

        public long ReadDataFromTAA1()
        {
            var anzTag = "ns=2;s=TAA1/Gesamttubenanzahl";
            var resetBitTag = "ns=2;s=TAA1/resetBit";
            try
            {
                var gesamtAnzahl = client.ReadNode(anzTag);
                var refeshBit = client.ReadNode(resetBitTag);
                if (gesamtAnzahl != null)
                {
                    return long.Parse(gesamtAnzahl.ToString());
                }
                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            return -1;
        }        //TAA2: anz: ns=4;i=11 | reset-Bit: ns=4;i=12

        public long ReadDataFromTAA2()
        {
            var anzTag = "ns=2;s=TAA2/Gesamttubenanzahl";
            var resetBitTag = "ns=2;s=TAA2/resetBitTag";
            try
            {
                var gesamtAnzahl = client.ReadNode(anzTag);
                var refeshBit = client.ReadNode(resetBitTag);
                if (gesamtAnzahl != null)
                {
                    return long.Parse(gesamtAnzahl.ToString());
                }
                Console.WriteLine("-----------------------\nempty Data read");
                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            return -1;
        }
        public long ReadDataFromTAA3()
        {
            var anzTag = "ns=2;s=TAA3/Gesamttubenanzahl";
            // var resetBitTag = "ns=2;s=TAA3/resetBitTag";
            try
            {
                var gesamtAnzahl = client.ReadNode(anzTag);
                if (gesamtAnzahl != null)
                {
                    return long.Parse(gesamtAnzahl.ToString());
                }
                Console.WriteLine("-----------------------\nempty Data read");
                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            return -1;
        }
        public long ReadDataFromTAA4()
        {
            var anzTag = "ns=2;s=TAA4/Gesamttubenanzahl";
            var resetBitTag = "ns=2;s=TAA4/resetBitTag";
            try
            {
                var gesamtAnzahl = client.ReadNode(anzTag);
                var refeshBit = client.ReadNode(resetBitTag);
                if (gesamtAnzahl != null)
                {
                    return long.Parse(gesamtAnzahl.ToString());
                }
                Console.WriteLine("-----------------------\nempty Data read");
                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            return -1;
        }

        public void ResetBit(string machine)
        {   //same node on every Machine except the Name of the Machine => TAA1 TAA2 ...
            client.WriteNode("ns=2;s=" + machine + "/resetBitTag", true);
            Thread.Sleep(2000);//so that the server can read the Bit 
            client.WriteNode("ns=2;s=" + machine + "/resetBitTag", false);
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
