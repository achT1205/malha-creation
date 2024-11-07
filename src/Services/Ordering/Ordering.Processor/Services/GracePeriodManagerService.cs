using MassTransit;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Application.Abstractions.Data;
using Ordering.Application.Orders.Commands.ConfirmGracePeriod;
using Ordering.Processor.Events.IntegrationEvents;
using Ordering.Processor.Models;

namespace Ordering.Processor.Services;

public class GracePeriodManagerService : BackgroundService
{
    private readonly BackgroundTaskOptions _options;
    private readonly ILogger _logger;
    private readonly IApplicationDbContext _context;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ISender _sender;


    public GracePeriodManagerService(
        ISender sender,
        IOptions<BackgroundTaskOptions> options,
        ILogger<GracePeriodManagerService> logger,
        IPublishEndpoint publishEndpoint,
        IApplicationDbContext dbContext)
    {
        _logger = logger;
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        _sender = sender;

    }
    //private readonly BackgroundTaskOptions _options = options?.Value ?? throw new ArgumentNullException(nameof(options));

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

        var orders = _context.Orders
                .Where(
                o => o.Status == Domain.Orders.Enums.OrderStatus.Pending &&
                DateTime.UtcNow.AddMinutes(-_options.GracePeriodTime
                ) >= o.CreatedAt).ToList();

        foreach (var o in orders)
        {
            var commad = new ConfirmGracePeriodCommand(o.Id.Value);

            await _sender.Send(commad);

            var confirmGracePeriodEvent = new GracePeriodConfirmedIntegrationEvent(o.Id.Value);

            _logger.LogInformation("Publishing integration event: {IntegrationEventId} - ({@IntegrationEvent})", confirmGracePeriodEvent.OrderId, confirmGracePeriodEvent);

            await _publishEndpoint.Publish(confirmGracePeriodEvent);
        }
    }
}
