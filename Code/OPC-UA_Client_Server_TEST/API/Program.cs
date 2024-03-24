using API.Maps;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using OPC_UA_Client;
using System.Timers;
using System;
using static System.Net.Mime.MediaTypeNames;
using Org.BouncyCastle.Security;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.IO.Pipes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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
app.UseDeveloperExceptionPage();
app.MapMachineData();
//var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();
//var lifetime = app.Lifetime;
//lifetime.ApplicationStopping.Register(() =>
//{
//    Console.WriteLine("Shutting down!");
//    MachineServices.SaveCurrentValuesIntoDB();
//    // Environment.Exit(0);
//});
Thread thread = new Thread(new ThreadStart(ListenForSignal));
thread.IsBackground = true;
thread.Start();
app.Run();
static async void ListenForSignal()
{
    var pipeClient = new NamedPipeClientStream(".", "MyShuttdownPipe", PipeDirection.InOut);

    try
    {
        Console.WriteLine("Connecting to named pipe server...");
        pipeClient.Connect();
        Console.WriteLine("Connected to named pipe server.");

        using (var reader = new StreamReader(pipeClient))
        {
            while (true)
            {
                string receivedData = reader.ReadLine()!;
                if (receivedData != null && receivedData.Length > 0)
                {
                    Console.WriteLine("Shutting down!");
                    MachineMaps.service.SaveCurrentValuesIntoProdVis();
                    ResetCheck();
                    Thread.Sleep(5000);
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
        Console.WriteLine($"Error: {ex.Message}");
    }
}
static void ResetCheck()
{
    if (DateTime.Now.TimeOfDay.Hours == 18 && DateTime.Now.TimeOfDay.Minutes <= 30)
    {
        MachineMaps.service.SaveValueIntoProdVisLongTerm();
        MachineMaps.service.ResetAllValuesAndCSV();
        MachineMaps.service = new MachineServices();
    }
}


