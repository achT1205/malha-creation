using System.Reflection;
using BuildingBlocks.Messaging.MassTransit;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMessageBroker(builder.Configuration, Assembly.GetExecutingAssembly());


var app = builder.Build();


app.Run();
