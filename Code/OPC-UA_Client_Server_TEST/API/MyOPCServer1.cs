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
    internal class MyOPCServer1
    {
        OpcServer server;
        List<OpcObjectNode> nodes = new List<OpcObjectNode>();

        public MyOPCServer1()
        {
              var taa1 = new OpcObjectNode(
            "TAA1",
            new OpcDataVariableNode<int>("Gesamttubenanzahl", value: 123),
            new OpcDataVariableNode<int>("resetBit", value: 1));
            nodes.Add(taa1);
            var taa2 = new OpcObjectNode(
            "TAA2",
            new OpcDataVariableNode<int>("Gesamttubenanzahl", value: 321),
            new OpcDataVariableNode<int>("resetBit", value: 0));
            nodes.Add(taa2);
            server = new OpcServer("opc.tcp://localhost:4840", nodes); //real-server: "opc.tcp://192.168.1.10:4840"
            
        }

        public void StartServer()
        {
            server.Start();
            Console.WriteLine("----------------------\nServer started!");
        }
        public void StopServer()
        {
            server.Stop();
            Console.WriteLine("---------------------\nServer stopped!");
        }
    }
}
