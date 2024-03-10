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
app.MapMachineData();
app.Run();
