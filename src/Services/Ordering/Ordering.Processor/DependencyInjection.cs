using Microsoft.Extensions.DependencyInjection;
using Ordering.Processor.Models;
using Ordering.Processor.Services;

namespace Ordering.Processor;

public static class DependencyInjection
{
    public static IServiceCollection AddProcessorServices(this IServiceCollection services) 
        => services.AddServices();

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddOptions<BackgroundTaskOptions>()
            .BindConfiguration(nameof(BackgroundTaskOptions));

        services.AddHostedService<GracePeriodManagerService>();

        return services;
    }

}