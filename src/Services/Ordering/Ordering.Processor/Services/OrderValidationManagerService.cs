using Ordering.Application.Orders.Commands.ValidateRoder;
using Ordering.Application.Orders.Queries.GetGraceTimeConfirmedOrders;

namespace Ordering.Processor.Services;

public class OrderValidationManagerService : BackgroundService
{
    private readonly BackgroundTaskOptions _options;
    private readonly ILogger _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IServiceScopeFactory _serviceScopeFactory;


    public OrderValidationManagerService(
        IOptions<BackgroundTaskOptions> options,
        ILogger<GracePeriodManagerService> logger,
        IServiceProvider serviceProvider,
        IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _serviceProvider = serviceProvider;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var delayTime = TimeSpan.FromSeconds(_options.CheckUpdateTime);

        if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogDebug("GracePeriodManagerService is starting.");
            cancellationToken.Register(() => _logger.LogDebug("GracePeriodManagerService background task is stopping."));
        }

        while (!cancellationToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug("GracePeriodManagerService background task is doing background work.");
            }

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

               var result = await mediator.Send(new GetGraceTimeConfirmedOrdersQuery(), cancellationToken);
                if (result != null && result.orders.Any()) {
                    foreach (var o in result.orders)
                    {
                        // implement all valivation here 

                        var commad = new ValidateRoderCommand(o.Id);

                        await mediator.Send(commad);
                    }
                }

            }

            await Task.Delay(delayTime, cancellationToken);
        }

        if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogDebug("GracePeriodManagerService background task is stopping.");
        }
    }    
}
