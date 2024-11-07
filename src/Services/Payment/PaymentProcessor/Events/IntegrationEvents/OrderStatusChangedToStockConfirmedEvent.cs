using BuildingBlocks.Messaging.Events;

namespace PaymentProcessor.Events.IntegrationEvents;

public record OrderStatusChangedToStockConfirmedEvent(Guid OrderId, string status) : IntegrationEvent;