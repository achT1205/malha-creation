using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using OcelotApiGateway.Middlewares;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
        builder.Services.AddOcelot();
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                builder =>
                {
                    builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
        });


        var app = builder.Build();


        app.UseCors();
        app.UseMiddleware<TokenCheckerMiddleware>();
        app.UseMiddleware<InterceptionMiddleware>();
        app.UseOcelot().Wait();
        app.Run();
    }
}