using Ordering.Domain.Orders.Enums;

namespace Ordering.Application.Orders.EventHandlers.Domain;
public class OrderUpdatedEventHandler(IPublishEndpoint publishEndpoint, ILogger<OrderUpdatedEventHandler> logger)
    : INotificationHandler<OrderStatusChangedEvent>
{
    public async Task Handle(OrderStatusChangedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        if (domainEvent.status == OrderStatus.Pending)
        {

        }

        if (domainEvent.status == OrderStatus.GracePeriodConfirmed)
        {

        }

        if (domainEvent.status == OrderStatus.Validated)
        {
            var evt = new OrderValidationSucceededEvent(domainEvent.order.Id.Value);

            await publishEndpoint.Publish(evt, cancellationToken);
        }

        if (domainEvent.status == OrderStatus.StockConfirmed)
        {

        }

        if (domainEvent.status == OrderStatus.Rejected)
        {

        }

        if (domainEvent.status == OrderStatus.Paid)
        {

        }

        if (domainEvent.status == OrderStatus.Shipped)
        {

        }

        if (domainEvent.status == OrderStatus.Shipped)
        {

        }

        if (domainEvent.status == OrderStatus.Completed)
        {

        }

        if (domainEvent.status == OrderStatus.Deleted)
        {

        }
    }
}