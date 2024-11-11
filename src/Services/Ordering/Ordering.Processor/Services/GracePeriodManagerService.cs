using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Application.Abstractions.Data;
using Ordering.Application.Orders.Commands.ConfirmGracePeriod;
using Ordering.Application.Orders.Queries.GetGraceTimeOrders;
using Ordering.Domain.Orders.Events;
using Ordering.Processor.Models;
using System.Threading;

namespace Ordering.Processor.Services;

public class GracePeriodManagerService : BackgroundService
{
    private readonly BackgroundTaskOptions _options;
    private readonly ILogger _logger;
    private readonly ISender _sender;
    private readonly IMediator _mediator;
    private readonly IServiceProvider _serviceProvider;


    public GracePeriodManagerService(
        ISender sender,
        IMediator mediator,
        IOptions<BackgroundTaskOptions> options,
        ILogger<GracePeriodManagerService> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _sender = sender;
        _mediator = mediator;
        _serviceProvider = serviceProvider;
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

            //await CheckConfirmedGracePeriodOrders();

            await Task.Delay(delayTime, stoppingToken);
        }

        if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogDebug("GracePeriodManagerService background task is stopping.");
        }
    }

    private async Task CheckConfirmedGracePeriodOrders()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();

            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug("Checking confirmed grace period orders");
            }
            var orders = await dbContext.Orders.Where(_ => _.GracePeriodEnd >= DateTime.UtcNow && _.Status == Domain.Orders.Enums.OrderStatus.Pending)
                      .ToListAsync();

            //var result = await _sender.Send(new GetGraceTimeOrdersQuery());

            foreach (var o in orders)
            {
                var commad = new ConfirmGracePeriodCommand(o.Id.Value);

                await _sender.Send(commad);

                var confirmGracePeriodEvent = new GracePeriodConfirmedDomainEvent(o.Id.Value);

                _logger.LogInformation("Publishing integration event: {IntegrationEventId} - ({@IntegrationEvent})", confirmGracePeriodEvent.OrderId, confirmGracePeriodEvent);

                await _mediator.Publish(confirmGracePeriodEvent);
            }
        }

      
    }
}
