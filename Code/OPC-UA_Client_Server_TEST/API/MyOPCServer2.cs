using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Opc.UaFx;
using Opc.UaFx.Server;

namespace OPC_UA_Client
{
    public class MyOPCServer2
    {
        OpcServer server;
        List<OpcObjectNode> nodes = new List<OpcObjectNode>();

        public MyOPCServer2(string url)
        {
            var taa3 = new OpcObjectNode(
          "TAA3",
          new OpcDataVariableNode<int>("Gesamttubenanzahl", value: 0),
          new OpcDataVariableNode<int>("resetBit", value: 1));
            nodes.Add(taa3);
            var taa4 = new OpcObjectNode(
            "TAA4",
            new OpcDataVariableNode<int>("Gesamttubenanzahl", value: 0),
            new OpcDataVariableNode<int>("resetBit", value: 0));
            nodes.Add(taa4);
            server = new OpcServer(url, nodes);

        }
        public void InitServer2Values(int taa3Val, int taa4Val)
        {
            var gesamttubenAnzTAA3 = server.NodeManagers[0].Nodes[0].Children().ToList()[0].AttributeValue<int>(OpcAttribute.Value);
            var resetBitValTAA3 = server.NodeManagers[0].Nodes[0].Children().ToList()[1].AttributeValue<int>(OpcAttribute.Value);
            server.NodeManagers[0].RemoveNode(server.NodeManagers[0].Nodes[0]);
            server.NodeManagers[0].AddNode(new OpcObjectNode(
      "TAA3",
      new OpcDataVariableNode<int>("Gesamttubenanzahl", value: taa3Val),
      new OpcDataVariableNode<int>("resetBit", value: resetBitValTAA3)));

            var gesamttubenAnzTAA4 = server.NodeManagers[0].Nodes[3].Children().ToList()[0].AttributeValue<int>(OpcAttribute.Value);
            var resetBitValTAA4 = server.NodeManagers[0].Nodes[3].Children().ToList()[1].AttributeValue<int>(OpcAttribute.Value);
            server.NodeManagers[0].RemoveNode(server.NodeManagers[0].Nodes[3]);
            server.NodeManagers[0].AddNode(new OpcObjectNode(
      "TAA4",
      new OpcDataVariableNode<int>("Gesamttubenanzahl", value: taa4Val),
                new OpcDataVariableNode<int>("resetBit", value: resetBitValTAA4)));
        }

        private void GesamttubenAnzRaiseMethod()
        {
            while (true)
            {
                //Console.WriteLine(server.NodeManagers[0].Nodes[0].Name);
                //Console.WriteLine(server.NodeManagers[0].Nodes[3].Name);
                var gesamttubenAnzTAA3 = server.NodeManagers[0].Nodes[0].Children().ToList()[0].AttributeValue<int>(OpcAttribute.Value);
                var resetBitValTAA3 = server.NodeManagers[0].Nodes[0].Children().ToList()[1].AttributeValue<int>(OpcAttribute.Value);
                server.NodeManagers[0].RemoveNode(server.NodeManagers[0].Nodes[0]);
                server.NodeManagers[0].AddNode(new OpcObjectNode(
          "TAA3",
          new OpcDataVariableNode<int>("Gesamttubenanzahl", value: gesamttubenAnzTAA3 + 1),
          new OpcDataVariableNode<int>("resetBit", value: resetBitValTAA3)));

                var gesamttubenAnzTAA4 = server.NodeManagers[0].Nodes[3].Children().ToList()[0].AttributeValue<int>(OpcAttribute.Value);
                var resetBitValTAA4 = server.NodeManagers[0].Nodes[3].Children().ToList()[1].AttributeValue<int>(OpcAttribute.Value);
                server.NodeManagers[0].RemoveNode(server.NodeManagers[0].Nodes[3]);
                server.NodeManagers[0].AddNode(new OpcObjectNode(
          "TAA4",
          new OpcDataVariableNode<int>("Gesamttubenanzahl", value: gesamttubenAnzTAA4 + 1),
                    new OpcDataVariableNode<int>("resetBit", value: resetBitValTAA4)));
                CheckResetBitMethod();
                Thread.Sleep(2000);
            }
        }

        private void CheckResetBitMethod()
        {
            // var node = server.NodeManagers[0].Nodes[0].Children().ToList()[1];

            if (server.NodeManagers[0].Nodes[0].Children().ToList()[1].AttributeValue(OpcAttribute.Value)!.ToString()!.Equals("1"))
            {
                server.NodeManagers[0].RemoveNode(server.NodeManagers[0].Nodes[0]);
                server.NodeManagers[0].AddNode(new OpcObjectNode(
                "TAA3",
          new OpcDataVariableNode<int>("Gesamttubenanzahl", value: 0),
          new OpcDataVariableNode<int>("resetBit", value: 0)));
            }
            if (server.NodeManagers[0].Nodes[3].Children().ToList()[1].AttributeValue(OpcAttribute.Value).ToString()!.Equals("1"))
            {
                server.NodeManagers[0].RemoveNode(server.NodeManagers[0].Nodes[3]);
                server.NodeManagers[0].AddNode(new OpcObjectNode(
          "TAA4",
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
