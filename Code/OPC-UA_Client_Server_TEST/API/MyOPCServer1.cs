using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
          new OpcDataVariableNode<int>("resetBit", value: 1));
            nodes.Add(taa1);
            var taa2 = new OpcObjectNode(
            "TAA2",
            new OpcDataVariableNode<int>("Gesamttubenanzahl", value: 1000),
            new OpcDataVariableNode<int>("resetBit", value: 0));
            nodes.Add(taa2);
            server = new OpcServer(url, nodes); //real-server: "opc.tcp://192.168.1.10:4840"

        }

        void MyThreadMethod()
        {
            var value1 = 99;
            var value2 = 1000;
            while (true)
            {
                Console.WriteLine(server.NodeManagers[0].Nodes[0].Name);
                Console.WriteLine(server.NodeManagers[0].Nodes[3].Name);
                server.NodeManagers[0].RemoveNode(server.NodeManagers[0].Nodes[0]);
                server.NodeManagers[0].AddNode(new OpcObjectNode(
          "TAA1",
          new OpcDataVariableNode<int>("Gesamttubenanzahl", value: value1),
          new OpcDataVariableNode<int>("resetBit", value: 1)));
                value1++;
                server.NodeManagers[0].RemoveNode(server.NodeManagers[0].Nodes[3]);
                server.NodeManagers[0].AddNode(new OpcObjectNode(
          "TAA2",
          new OpcDataVariableNode<int>("Gesamttubenanzahl", value: value2),
          new OpcDataVariableNode<int>("resetBit", value: 1)));
                value2++;
                Thread.Sleep(200);
            }

        }

        public void StartServer()
        {
            server.Start();
            Thread myThread = new Thread(new ThreadStart(MyThreadMethod));

            myThread.Start();
            Console.WriteLine("----------------------\nServer started!");
        }
        public void StopServer()
        {
            server.Stop();
            Console.WriteLine("---------------------\nServer stopped!");
        }
    }
}
