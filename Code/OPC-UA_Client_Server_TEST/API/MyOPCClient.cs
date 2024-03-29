﻿using Opc.Ua;
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
                Disconnect();
                client.Connect();
                Console.WriteLine("-----------------------\nconnected!");
             //       var node = client.BrowseNode(OpcObjectTypes.ObjectsFolder); //refresh-Bit Tag: ns=4;i=7
             //     Browse(node);
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
                if (client.State != OpcClientState.Connected) return;
                client.Disconnect();
                Console.WriteLine("-----------------------\ndisconnected!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }

        public int ReadDataFromTAA1()
        {
            var anzTag = "ns=2;s=TAA1/Gesamttubenanzahl";
            try
            {
                var gesamtAnzahl = client.ReadNode(anzTag);
                if (gesamtAnzahl != null)
                {
                    return int.Parse(gesamtAnzahl.ToString());
                }
                return ReadDataFromTAA1();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            return -1;
        }

        public int ReadDataFromTAA2()
        {
            var anzTag = "ns=2;s=TAA2/Gesamttubenanzahl";
            try
            {
                var gesamtAnzahl = client.ReadNode(anzTag);
                if (gesamtAnzahl != null)
                {
                    return int.Parse(gesamtAnzahl.ToString());
                }
                Console.WriteLine("-----------------------\nempty Data read");
                return ReadDataFromTAA2();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            return -1;
        }
        public int ReadDataFromTAA3()
        {
            var anzTag = "ns=2;s=TAA3/Gesamttubenanzahl";
            try
            {
                var gesamtAnzahl = client.ReadNode(anzTag);
                if (gesamtAnzahl != null)
                {
                    return int.Parse(gesamtAnzahl.ToString());
                }
                Console.WriteLine("-----------------------\nempty Data read");
                return ReadDataFromTAA3();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            return -1;
        }
        public int ReadDataFromTAA4()
        {
            var anzTag = "ns=2;s=TAA4/Gesamttubenanzahl";
            try
            {
                var gesamtAnzahl = client.ReadNode(anzTag);
                if (gesamtAnzahl != null)
                {
                    return int.Parse(gesamtAnzahl.ToString());
                }
                Console.WriteLine("-----------------------\nempty Data read");
                return ReadDataFromTAA4();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            return -1;
        }

        public void ResetBit(string machine)
        {   //same node on every Machine except the Name of the Machine => TAA1 TAA2 ...
            client.WriteNode("ns=2;s=" + machine + "/resetBit", 1); // ns=2;s=TAA1/resetBit
            Thread.Sleep(2000);//so that the server can read the Bit 
            client.WriteNode("ns=2;s=" + machine + "/resetBit", 0);
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
