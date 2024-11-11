namespace BuildingBlocks.Messaging.Events;
public record OrderValidationSucceededEvent(Guid OrderId) : IntegrationEvent;