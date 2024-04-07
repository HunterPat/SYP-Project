using API.Maps;
using API.Services;
using OPC_UA_Client;
using System.Diagnostics;
using System.IO.Pipes;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<MachineServices>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
Console.WriteLine("Ready!");
app.MapMachineData();
Thread thread = new Thread(new ThreadStart(ListenForSignal));
thread.IsBackground = true;

thread.Start();
app.Run();
static async void ListenForSignal()
{
    var pipeClient = new NamedPipeClientStream(".", "MyShuttdownPipe", PipeDirection.InOut);


    try
    {
        pipeClient.Connect();
        Console.WriteLine("Connected to named pipe server.");

        using (var reader = new StreamReader(pipeClient))
        {
            while (true)
            {
                string receivedData = reader.ReadLine()!;
                if (receivedData != null && receivedData.Length > 0)
                {
                    Console.WriteLine($"Data received from server: {receivedData}");
                    Console.WriteLine("Shutting down!");
                    MachineMaps.service.SaveCurrentValuesIntoProdVis();
                    ResetCheck();
                  //  Thread.Sleep(5000);
                    pipeClient.Close();
                    await pipeClient.DisposeAsync();
                    pipeClient = null!;
                    Environment.Exit(0);

                }
            }
        }
    }
    catch (Exception ex)
    {
        // Log or handle the exception
        Console.WriteLine($"Error in Listen for Server signals: {ex.Message}");
    }
}
static void ResetCheck()
{
    if (DateTime.Now.TimeOfDay.Hours == 18 && DateTime.Now.TimeOfDay.Minutes <= 20)
    {
        MachineMaps.service.SaveValueIntoProdVisLongTerm();
        MachineMaps.service.ResetAllValuesAndCSV();
        MachineMaps.service = new MachineServices();
    }
}
