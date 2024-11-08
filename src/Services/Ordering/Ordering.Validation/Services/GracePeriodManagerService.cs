using MassTransit;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Application.Orders.Commands.ValidationOrder;
using Ordering.Application.Orders.Queries.GetGraceTimeConfirmedOrders;
using Ordering.Validation.Events.IntegrationEvents;

namespace Ordering.Validation.Services;

public class OrderValidationManagerService : BackgroundService
{
    //private readonly BackgroundTaskOptions _options;
    private readonly ILogger _logger;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ISender _sender;


    public OrderValidationManagerService(
        ISender sender,
        ILogger<OrderValidationManagerService> logger,
        IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        _sender = sender;

    }
    //private readonly BackgroundTaskOptions _options = options?.Value ?? throw new ArgumentNullException(nameof(options));

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var delayTime = TimeSpan.FromSeconds(30);

        if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogDebug("GracePeriodManagerService is starting.");
            stoppingToken.Register(() => _logger.LogDebug("GracePeriodManagerService background task is stopping."));
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug("GracePeriodManagerService background task is doing background work.");
            }

            await CheckConfirmedGracePeriodOrders();

            await Task.Delay(delayTime, stoppingToken);
        }

        if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogDebug("GracePeriodManagerService background task is stopping.");
        }
    }

    private async Task CheckConfirmedGracePeriodOrders()
    {

        if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogDebug("Checking confirmed grace period orders");
        }

        var result = await _sender.Send(new GetGraceTimeConfirmedOrdersQuery());

        foreach (var o in result.orders)
        {
            var commad = new ValidationOrderCommand(o.Id);

            await _sender.Send(commad);

            var evt = new OrdarValidateEventIntegration(o.Id);

            _logger.LogInformation("Publishing integration event: {IntegrationEventId} - ({@IntegrationEvent})", confirmGracePeriodEvent.OrderId, confirmGracePeriodEvent);

            await _publishEndpoint.Publish(evt);
        }
    }
}
