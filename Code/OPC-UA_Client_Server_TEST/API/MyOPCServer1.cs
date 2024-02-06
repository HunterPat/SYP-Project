using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;
using Opc.Ua;
using Opc.UaFx;
using Opc.UaFx.Server;

namespace OPC_UA_Client
{
    internal class MyOPCServer1
    {
        OpcServer server;
        List<OpcObjectNode> nodes = new List<OpcObjectNode>();

        public MyOPCServer1(string url)
        {
            var taa1 = new OpcObjectNode(
          "TAA1",
          new OpcDataVariableNode<int>("Gesamttubenanzahl", value: 99),
          new OpcDataVariableNode<int>("resetBit", value: 0));
            nodes.Add(taa1);
            var taa2 = new OpcObjectNode(
            "TAA2",
            new OpcDataVariableNode<int>("Gesamttubenanzahl", value: 1000),
            new OpcDataVariableNode<int>("resetBit", value: 0));
            nodes.Add(taa2);
            server = new OpcServer(url, nodes); //real-server: "opc.tcp://192.168.1.10:4840"

        }

        private void GesamttubenAnzRaiseMethod()
        {

            while (true)
            {
                //Console.WriteLine(server.NodeManagers[0].Nodes[0].Name);
                //Console.WriteLine(server.NodeManagers[0].Nodes[3].Name);
                var gesamttubenAnzTAA1 = server.NodeManagers[0].Nodes[0].Children().ToList()[0].AttributeValue<int>(OpcAttribute.Value);
                var resetBitValTAA1 = server.NodeManagers[0].Nodes[0].Children().ToList()[1].AttributeValue<int>(OpcAttribute.Value);
                server.NodeManagers[0].RemoveNode(server.NodeManagers[0].Nodes[0]);
                server.NodeManagers[0].AddNode(new OpcObjectNode(
          "TAA1",
          new OpcDataVariableNode<int>("Gesamttubenanzahl", value: gesamttubenAnzTAA1 + 1),
          new OpcDataVariableNode<int>("resetBit", value: resetBitValTAA1)));
                var gesamttubenAnzTAA2 = server.NodeManagers[0].Nodes[3].Children().ToList()[0].AttributeValue<int>(OpcAttribute.Value);
                var resetBitValTAA2 = server.NodeManagers[0].Nodes[3].Children().ToList()[1].AttributeValue<int>(OpcAttribute.Value);
                server.NodeManagers[0].RemoveNode(server.NodeManagers[0].Nodes[3]);
                server.NodeManagers[0].AddNode(new OpcObjectNode(
          "TAA2",
          new OpcDataVariableNode<int>("Gesamttubenanzahl", value: gesamttubenAnzTAA2 + 1),
                    new OpcDataVariableNode<int>("resetBit", value: resetBitValTAA2)));
                CheckResetBitMethod();
                Thread.Sleep(2000);
            }
        }

        private void CheckResetBitMethod()
        {
            // var node = server.NodeManagers[0].Nodes[0].Children().ToList()[1];

            if (server.NodeManagers[0].Nodes[0].Children().ToList()[1].AttributeValue(OpcAttribute.Value).ToString().Equals("1"))
            {
                server.NodeManagers[0].RemoveNode(server.NodeManagers[0].Nodes[0]);
                server.NodeManagers[0].AddNode(new OpcObjectNode(
                "TAA1",
          new OpcDataVariableNode<int>("Gesamttubenanzahl", value: 0),
          new OpcDataVariableNode<int>("resetBit", value: 0)));
            }
            if (server.NodeManagers[0].Nodes[3].Children().ToList()[1].AttributeValue(OpcAttribute.Value).ToString().Equals("1"))
            {

                server.NodeManagers[0].RemoveNode(server.NodeManagers[0].Nodes[3]);
                server.NodeManagers[0].AddNode(new OpcObjectNode(
          "TAA2",
          new OpcDataVariableNode<int>("Gesamttubenanzahl", value: 0),
                    new OpcDataVariableNode<int>("resetBit", value: 0)));
            }
        }

        public void StartServer()
        {
            server.Start();
            Thread gesamttubenAnzRaiseThread = new Thread(new ThreadStart(GesamttubenAnzRaiseMethod));
            gesamttubenAnzRaiseThread.Start();
            Console.WriteLine("----------------------\nServer started!");
        }

        public void StopServer()
        {
            server.Stop();
            Console.WriteLine("---------------------\nServer stopped!");
        }
    }
}
