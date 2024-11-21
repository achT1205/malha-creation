using BuildingBlocks.Middlewares;
using Ordering.API;
using Ordering.API.Endpoints;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extentions;
using Ordering.Processor;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services
            .AddApplicationServices(builder.Configuration)
            .AddInfrastructureServices(builder.Configuration)
            .AddApiServices(builder.Configuration)
            .AddProcessorServices();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseApiServices();

        if (app.Environment.IsDevelopment())
        {
            app.InitialiseDatabase();
        }
        Stripe.StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

        //app.UseMiddleware<RetrictAccessMiddleware>();

        app.MapOrderEndpoints();

        app.Run();
    }
}