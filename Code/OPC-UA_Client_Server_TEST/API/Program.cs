using Microsoft.AspNetCore.Mvc;
using OPC_UA_Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Server
int gesamtTubenAnzZiel = 0;
string server1URL = "opc.tcp://localhost:4840/";
string server2URL = "opc.tcp://localhost:4841/";
MyOPCServer1 server1 = new MyOPCServer1(server1URL);
MyOPCServer2 server2 = new MyOPCServer2(server2URL);
server1.StartServer();
server2.StartServer();
//Client
MyOPCClient client = new MyOPCClient(server1URL);
client.EstablishConnection();

app.MapGet("/{serverID}/TAA1", (int serverID) =>
{
    if(serverID == 1)
    {
        if(client.serverURL != server1URL)
        {
            client.Disconnect();
            client = new MyOPCClient(server1URL);
            client.EstablishConnection();
        }
        return client.ReadDataFromTAA1().Value;

    }else if(serverID == 2)
    {
        if (client.serverURL != server2URL)
        {
            client.Disconnect();
            client = new MyOPCClient(server2URL);
            client.EstablishConnection();
        }
        return client.ReadDataFromTAA1().Value;
    }
    return null;
});
app.MapGet("/{serverID}/TAA2", (int serverID) =>
{
    if (serverID == 1)
    {
        if (client.serverURL != server1URL)
        {
            client.Disconnect();
            client = new MyOPCClient(server1URL);
            client.EstablishConnection();
        }
        return client.ReadDataFromTAA2().Value;

    }
    else if (serverID == 2)
    {
        if (client.serverURL != server2URL)
        {
            client.Disconnect();
            client = new MyOPCClient(server2URL);
            client.EstablishConnection();
        }
        return client.ReadDataFromTAA2().Value;
    }
    return null;
});
var gesamtTubenAnzGroup = app.MapGroup("/gesamttubenanz");
gesamtTubenAnzGroup.MapGet("", () => gesamtTubenAnzZiel);

gesamtTubenAnzGroup.MapPost("", ([FromBody] int value) =>
{
    gesamtTubenAnzZiel = value;
    return value;
});
app.Run();
public class GesamtTubenAnzZiel
{
    public int GesamtTubenAnzZielValue { get; set; } = 0;
}