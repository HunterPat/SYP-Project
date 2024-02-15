using API.Maps;
using API.Services;
using OPC_UA_Client;
using System.Data.SQLite;

SQLiteConnection.CreateFile("ProdVis.sqlite"); //Check If can create here TODO: comment

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
app.Run();
