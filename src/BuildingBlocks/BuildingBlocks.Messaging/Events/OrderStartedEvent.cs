namespace BuildingBlocks.Messaging.Events;

public record OrderStartedEvent : IntegrationEvent
{
    public Guid UserId { get; set; } = default!;
}