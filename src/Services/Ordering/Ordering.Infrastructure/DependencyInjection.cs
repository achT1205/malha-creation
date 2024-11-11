using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Interceptors;
using Ordering.Infrastructure.Services;

namespace Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
    this IServiceCollection services,
    IConfiguration configuration) =>
    services
        .AddServices(configuration)
        .AddDatabase(configuration)
        .AddHealthChecks(configuration);


    private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.
        //services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        // Bind the ExternalApiSettings from appsettings.json
        services.Configure<ExternalApiSettings>(configuration.GetSection("ExternalApiSettings"));

        // Register HttpClient
        services.AddHttpClient();

        // Register the external API service
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICartService, CartService>();


        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

        return services;
    }


    private static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHealthChecks()
            .AddSqlServer(configuration.GetConnectionString("Database")!);

        return services;
    }
}