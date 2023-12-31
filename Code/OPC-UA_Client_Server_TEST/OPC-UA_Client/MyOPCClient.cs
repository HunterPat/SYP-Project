﻿using Opc.Ua;
using Opc.UaFx;
using Opc.UaFx.Client;
namespace OPC_UA_Client
{
    internal class MyOPCClient
    {
        OpcClient client;
        public MyOPCClient()
        {
            client = new OpcClient("opc.tcp://localhost:4840");//OPC-Server URL
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

        public OpcValue ReadDataFromTAA1()
        {
            var anzTag = "ns=2;s=TAA1/Gesamttubenanzahl";
            var resetBitTag = "ns=2;s=TAA1/resetBit";
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
        }        //TAA2: anz: ns=4;i=11 | reset-Bit: ns=4;i=12

        public OpcValue ReadDataFromTAA2()
        {
            var anzTag = "ns=2;s=TAA2/Gesamttubenanzahl";
            var resetBitTag = "ns=2;s=TAA2/resetBitTag";
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
