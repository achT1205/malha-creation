namespace Ordering.Application.Orders.Events;

public record OrderPaymentSucceededIntegrationEvent(Guid OrderId) : IntegrationEvent;