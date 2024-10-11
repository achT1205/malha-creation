using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;

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

        //if (app.Environment.IsDevelopment())
        //{
        //    app.InitialiseDatabase();
        //}

        app.Run();
    }
}