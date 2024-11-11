namespace BuildingBlocks.Messaging.Events;
public record OrderStockConfirmedEvent(Guid OrderId) : IntegrationEvent;