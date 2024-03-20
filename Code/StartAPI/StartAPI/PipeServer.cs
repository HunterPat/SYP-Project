using System.Diagnostics;
using System.IO.Pipes;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Timers;

namespace StartAPI
{
    public class PipeServer
    {
        System.Timers.Timer restartTimer = new System.Timers.Timer();
        private string finalPath;
        public PipeServer()
        {
            var currentDirectory = Directory.GetCurrentDirectory();

            for (int i = 0; i < 5; i++)
            {
                currentDirectory = Directory.GetParent(currentDirectory).FullName;
            }
            

            // Append the remaining path
            finalPath = currentDirectory + @"\OPC-UA_Client\API\bin\Debug\net8.0\API.exe";
            Process.Start(finalPath);
        }
        private NamedPipeServerStream pipeServer;

        public void Start()
        {
            //Thread thread = new Thread(new ThreadStart(StartServer));
            //thread.Start();
            pipeServer?.Dispose();
            pipeServer = null!;
            PipeSecurity pipeSecurity = new PipeSecurity();
            pipeSecurity.AddAccessRule(new PipeAccessRule(new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null), PipeAccessRights.ReadWrite, AccessControlType.Allow));
            pipeServer = NamedPipeServerStreamConstructors.New("MyShuttdownPipe", PipeDirection.InOut, NamedPipeServerStream.MaxAllowedServerInstances, PipeTransmissionMode.Byte, PipeOptions.Asynchronous | PipeOptions.WriteThrough, 0, 0, pipeSecurity);
            pipeServer.BeginWaitForConnection(ConnectionCallback, null);
            Console.WriteLine("Named pipe server created. Waiting for connection...");
        }

        private void ConnectionCallback(IAsyncResult result)
        {
            try
            {
                pipeServer?.EndWaitForConnection(result);
                Console.WriteLine("Client connected.");
                StartRestartTimer();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
        private void StartRestartTimer()
        {
            // Dispose of any existing timer
            restartTimer?.Dispose();

            // Start a new timer for restarting the application
            restartTimer = new System.Timers.Timer();
            restartTimer.Interval = 10000; // Adjust as needed
            restartTimer.Elapsed += RestartApplication!;
            restartTimer.AutoReset = true;
            restartTimer.Start();
        }
        void RestartApplication(object sender, ElapsedEventArgs e)
        {
            try { 
            Console.WriteLine("+----------------------------------+\n|    Restarting the application... |\n+----------------------------------+");
            SendMessageToClient("END");

            if (Process.GetProcessesByName("API").Length > 0)
            {
                SendMessageToClient("END");
                Thread.Sleep(3000);
                if (Process.GetProcessesByName("API").Length > 0)
                {
                    Process.GetProcessesByName("API").First().Kill();
                }
            }
            //pipeServer.Disconnect();

            pipeServer?.Dispose();
            pipeServer = null!;
            Start();
            pipeServer.BeginWaitForConnection(ConnectionCallback, null);
            Process.Start(finalPath);
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //Start();
            }
        }

        public void SendMessageToClient(string message)
        {
            try
            {
                if (pipeServer != null && pipeServer.IsConnected)
                {
                    using (var writer = new StreamWriter(pipeServer))
                    {
                        writer.WriteLine(message);
                        writer.Flush();
                    }
                }
                else
                {
                  //  Console.WriteLine("Client not connected. Message not sent.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("SendMessageToClinet: " + e.Message);
            }
        }
    }

}
