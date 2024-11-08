namespace Ordering.Application.Orders.IntegrationEvents;

public record OrderStartedEvent : IntegrationEvent
{
    public Guid UserId { get; init; }

    public OrderStartedEvent(Guid userId)
        => UserId = userId;
}
