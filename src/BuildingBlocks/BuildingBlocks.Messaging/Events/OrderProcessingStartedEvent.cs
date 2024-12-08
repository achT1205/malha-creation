namespace BuildingBlocks.Messaging.Events;
public record OrderProcessingStartedEvent(Guid OrderId) : IntegrationEvent;