using Ordering.Application.Orders.Commands.ProcessOrder;
using Ordering.Application.Orders.Queries.GetGraceTimeConfirmedOrders;

namespace Ordering.Processor.Services;

public class OrderProcessingManagerService : BackgroundService
{
    private readonly BackgroundTaskOptions _options;
    private readonly ILogger _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;


    public OrderProcessingManagerService(
        IOptions<BackgroundTaskOptions> options,
        ILogger<OrderProcessingManagerService> logger,
        IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var delayTime = TimeSpan.FromSeconds(_options.CheckUpdateTime);

        if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogDebug("ProcessingManagerService is starting.");
            cancellationToken.Register(() => _logger.LogDebug("ProcessingManagerService background task is stopping."));
        }

        while (!cancellationToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug("ProcessingManagerService background task is doing background work.");
            }

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

               var result = await mediator.Send(new GetGraceTimeConfirmedOrdersQuery(), cancellationToken);
                if (result != null && result.orders.Any()) {
                    foreach (var o in result.orders)
                    {
                        var commad = new ProcessOrderCommand(o.Id);
                        await mediator.Send(commad);
                    }
                }

            }

            await Task.Delay(delayTime, cancellationToken);
        }

        if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogDebug("ProcessingManagerService background task is stopping.");
        }
    }    
}
