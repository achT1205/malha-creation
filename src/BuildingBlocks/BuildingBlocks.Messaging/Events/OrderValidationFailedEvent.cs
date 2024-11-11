namespace BuildingBlocks.Messaging.Events;

public record OrderValidationFailed(Guid OrderId) : IntegrationEvent;