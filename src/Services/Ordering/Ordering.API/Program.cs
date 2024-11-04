using BuildingBlocks.Middlewares;
using Ordering.API;
using Ordering.API.Endpoints;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extentions;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services
            .AddApplicationServices(builder.Configuration)
            .AddInfrastructureServices(builder.Configuration)
            .AddApiServices(builder.Configuration);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseApiServices();

        if (app.Environment.IsDevelopment())
        {
            app.InitialiseDatabase();
        }

        app.UseMiddleware<RetrictAccessMiddleware>();

        app.MapOrderEndpoints();

        app.Run();
    }
}