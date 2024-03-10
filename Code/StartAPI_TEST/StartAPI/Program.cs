using System.Diagnostics;
using System.Timers;
var currentDirectory = Directory.GetCurrentDirectory();
for (int i = 0; i < 5; i++)
{
    currentDirectory = Directory.GetParent(currentDirectory).FullName;
}

// Append the remaining path
string finalPath = currentDirectory + @"\OPC-UA_Client_Server_TEST\API\bin\Debug\net8.0\API.exe";
Console.WriteLine("+----------------------------------+\n|    Starting the Application...   |\n+----------------------------------+");
Process.Start(finalPath);
Console.WriteLine($"\nFinal Path: {finalPath}\n");
System.Timers.Timer restartTimer = new System.Timers.Timer();
restartTimer.Interval = 20 * 60 * 1000; // 30 * 60 * 1000
restartTimer.Elapsed += RestartApplication!;
restartTimer.AutoReset = true;
restartTimer.Start();
while (true) { }
void RestartApplication(object sender, ElapsedEventArgs e)
{
    Console.WriteLine("+----------------------------------+\n|    Restarting the application... |\n+----------------------------------+");
    if (Process.GetProcessesByName("API").Length > 0) Process.GetProcessesByName("API").First().Kill();

    Process.Start(finalPath);
}