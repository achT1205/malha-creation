using BuildingBlocks.Exceptions.Handler;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Catalog.API;


public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddSwaggerGen();
        services.AddExceptionHandler<CustomExceptionHandler>();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigins", policy =>
            {
                policy.WithOrigins("http://localhost:5173", "http://localhost:5175")
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials(); // Optional, only if cookies or credentials are needed
            });
        });

        //Grpc Services
        //services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
        //{
        //    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
        //})
        //.ConfigurePrimaryHttpMessageHandler(() =>
        //{
        //    var handler = new HttpClientHandler
        //    {
        //        ServerCertificateCustomValidationCallback =
        //       HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        //    };
        //    return handler;
        //});


        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            //app.UseSwagger();
            //app.UseSwaggerUI();
        }

        app.UseExceptionHandler(options => { });
        app.UseHealthChecks("/health",
            new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
        // Use CORS middleware
        app.UseCors("AllowSpecificOrigins");
        return app;
    }
}