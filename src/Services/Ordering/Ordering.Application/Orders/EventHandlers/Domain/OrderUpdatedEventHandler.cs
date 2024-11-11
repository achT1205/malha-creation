namespace Ordering.Application.Orders.EventHandlers.Domain;
public class OrderUpdatedEventHandler(IPublishEndpoint publishEndpoint, ILogger<OrderUpdatedEventHandler> logger)
    : INotificationHandler<OrderStatusChangedEvent>
{
    public async Task Handle(OrderStatusChangedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        if (domainEvent.status == Ordering.Domain.Orders.Enums.OrderStatus.GracePeriodConfirmed)
        {

        }

        if (domainEvent.status == Ordering.Domain.Orders.Enums.OrderStatus.Validated)
        {
            var evt = new OrderValidationSucceededEvent(domainEvent.order.Id.Value);

            await publishEndpoint.Publish(evt, cancellationToken);
        }

        if (domainEvent.status == Ordering.Domain.Orders.Enums.OrderStatus.StockConfirmed)
        {
            var evt = new StartOrderPaymentEvent(domainEvent.order.Id.Value);

            await publishEndpoint.Publish(evt, cancellationToken);
        }

        if (domainEvent.status == Ordering.Domain.Orders.Enums.OrderStatus.Rejected)
        {

        }

        if (domainEvent.status == Ordering.Domain.Orders.Enums.OrderStatus.Paid)
        {

        }
    }
}