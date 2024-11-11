namespace BuildingBlocks.Messaging.Events;
public record StartOrderPaymentEvent(Guid OrderId) : IntegrationEvent;