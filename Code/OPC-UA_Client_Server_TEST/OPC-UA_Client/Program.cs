
using Opc.UaFx;
using OPC_UA_Client;

public class Project
{
    public static void Main(String[] args) // TODO: change variable every 1 sec
    {
        //Server
        MyOPCServer server = new MyOPCServer();
        server.StartServer();
        //Client
        MyOPCClient client = new MyOPCClient();
        client.EstablishConnection();
        while (true)
        {
            var readDataTAA1 = client.ReadDataFromTAA1(); 
            var readDataTAA2 = client.ReadDataFromTAA2();
            if (readDataTAA1.Value != null)
            {
                Console.WriteLine("----------------------\nreadData TAA1: " + readDataTAA1.ToString());
            }
            else
            {
                Console.WriteLine("----------------------\nreadDataTAA1 is empty!");
            }

            if (readDataTAA2.Value != null)
            {
                Console.WriteLine("----------------------\nreadData TAA2: " + readDataTAA2.ToString());
            }
            else
            {
                Console.WriteLine("----------------------\nreadDataTAA2 is empty!");
            }
            Thread.Sleep(1000); //ajust
        }
    }
}