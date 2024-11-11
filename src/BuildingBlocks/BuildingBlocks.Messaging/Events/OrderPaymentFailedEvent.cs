namespace BuildingBlocks.Messaging.Events;

public record OrderPaymentFailedEvent(Guid OrderId) : IntegrationEvent;