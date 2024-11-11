namespace BuildingBlocks.Messaging.Events;
public record OrderPaymentSucceededEvent(Guid OrderId) : IntegrationEvent;