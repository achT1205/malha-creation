using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using OcelotApiGateway.Middlewares;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var  path = builder.Configuration["Ocelot:Path"]!;
        builder.Configuration.AddJsonFile(path, optional: false, reloadOnChange: true);
        builder.Services.AddOcelot().AddCacheManager(x =>
        {
            x.WithDictionaryHandle();
        });
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
        app.UseMiddleware<InterceptionMiddleware>();
        app.UseMiddleware<TokenCheckerMiddleware>();
        app.UseOcelot().Wait();
        app.Run();
    }
}