using OPC_UA_Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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
string server1URL = "opc.tcp://192.168.1.10:4840";
string server2URL = "opc.tcp://192.168.1.10:4840"; //TODO: change URL
MyOPCClient client = new MyOPCClient(server1URL);
client.EstablishConnection();
app.MapGet("/{serverID}/TAA1", (int serverID) =>
{
    if (serverID == 1)
    {
        if (client.serverURL != server1URL)
        {
            client.Disconnect();
            client = new MyOPCClient(server1URL);
            client.EstablishConnection();
        }
        return client.ReadDataFromTAA1().Value;

    }
    else if (serverID == 2)
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

app.Run();
