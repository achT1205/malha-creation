using BuildingBlocks.Messaging.Events;

namespace Ordering.Processor.Events.IntegrationEvents;

public record GracePeriodConfirmedIntegrationEvent : IntegrationEvent
{
    public Guid OrderId { get; }

    public GracePeriodConfirmedIntegrationEvent(Guid orderId) =>
        OrderId = orderId;
}