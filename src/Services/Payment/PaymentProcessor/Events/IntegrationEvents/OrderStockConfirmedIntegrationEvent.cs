using BuildingBlocks.Messaging.Events;

namespace PaymentProcessor.Events.IntegrationEvents;

public record OrderStockConfirmedIntegrationEvent(Guid OrderId) : IntegrationEvent;