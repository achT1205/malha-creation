namespace Ordering.Application.Orders.EventHandlers.Domain;
public class OrderUpdatedEventHandler(IPublishEndpoint publishEndpoint, ILogger<OrderUpdatedEventHandler> logger)
    : INotificationHandler<OrderStatusChangedEvent>
{
    public async Task Handle(OrderStatusChangedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        if (domainEvent.status == Ordering.Domain.Orders.Enums.OrderStatus.StockConfirmed)
        {
            var evt = new OrderStatusChangedToStockConfirmedEvent(domainEvent.order.Id.Value, domainEvent.status.ToString());

            await publishEndpoint.Publish(evt, cancellationToken);
        }
    }
}