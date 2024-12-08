using Ordering.Application.Orders.Commands.PackOrder;
using Ordering.Application.Orders.Commands.ProcessOrder;
using Ordering.Application.Orders.Queries.GetStockConfirmedOrders;

namespace Ordering.Processor.Services;

public class OrderPackingManagerService : BackgroundService
{
    private readonly BackgroundTaskOptions _options;
    private readonly ILogger _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;


    public OrderPackingManagerService(
        IOptions<BackgroundTaskOptions> options,
        ILogger<OrderPackingManagerService> logger,
        IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var delayTime = TimeSpan.FromSeconds(_options.PackingTime);

        if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogDebug("OrderPackingManagerService is starting.");
            cancellationToken.Register(() => _logger.LogDebug("OrderPackingManagerService background task is stopping."));
        }

        while (!cancellationToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug("OrderPackingManagerService background task is doing background work.");
            }

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

               var result = await mediator.Send(new GetStockConfirmedOrdersQuery(), cancellationToken);
                if (result != null && result.orders.Any()) {
                    foreach (var o in result.orders)
                    {
                        var commad = new PackOrderCommand(o.Id);
                        await mediator.Send(commad);
                    }
                }

            }

            await Task.Delay(delayTime, cancellationToken);
        }

        if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogDebug("OrderPackingManagerService background task is stopping.");
        }
    }    
}
