using MassTransit;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Application.Orders.Commands.ConfirmGracePeriod;
using Ordering.Application.Orders.Queries.GetGraceTimeOrders;
using Ordering.Domain.Orders.Events;
using Ordering.Processor.Models;

namespace Ordering.Processor.Services;

public class GracePeriodManagerService : BackgroundService
{
    private readonly BackgroundTaskOptions _options;
    private readonly ILogger _logger;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ISender _sender;


    public GracePeriodManagerService(
        ISender sender,
        IOptions<BackgroundTaskOptions> options,
        ILogger<GracePeriodManagerService> logger,
        IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        _sender = sender;

    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var delayTime = TimeSpan.FromSeconds(_options.CheckUpdateTime);

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

        var result = await _sender.Send(new GetGraceTimeOrdersQuery());

        foreach (var o in result.orders)
        {
            var commad = new ConfirmGracePeriodCommand(o.Id);

            await _sender.Send(commad);

            var confirmGracePeriodEvent = new GracePeriodConfirmedDomainEvent(o.Id);

            _logger.LogInformation("Publishing integration event: {IntegrationEventId} - ({@IntegrationEvent})", confirmGracePeriodEvent.OrderId, confirmGracePeriodEvent);

            await _publishEndpoint.Publish(confirmGracePeriodEvent);
        }
    }
}
