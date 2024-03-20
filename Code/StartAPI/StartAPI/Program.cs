using StartAPI;
using System.Diagnostics;
using System.Timers;
PipeServer pipe = new PipeServer();
pipe.Start();
Console.WriteLine("+----------------------------------+\n|    Starting the Application...   |\n+----------------------------------+");
//Process.Start(finalPath);
//Console.WriteLine($"\nFinal Path: {finalPath}\n");

while (true) { }