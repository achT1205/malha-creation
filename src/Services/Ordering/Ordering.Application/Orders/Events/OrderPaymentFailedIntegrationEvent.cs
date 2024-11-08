namespace Ordering.Application.Orders.Events;
public record OrderPaymentFailedIntegrationEvent(Guid OrderId) : IntegrationEvent;
