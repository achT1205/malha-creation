namespace Ordering.Application.Orders.EventHandlers.Domain;
public class OrderUpdatedEventHandler(IPublishEndpoint publishEndpoint, ILogger<OrderUpdatedEventHandler> logger)
    : INotificationHandler<OrderStatusChangedEvent>
{
    public async Task Handle(OrderStatusChangedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);
        if (domainEvent.status == OrderStatus.Processing)
        {
            var evt = new OrderProcessingStartedEvent(domainEvent.order.Id.Value);

            await publishEndpoint.Publish(evt, cancellationToken);
        }
    }
}