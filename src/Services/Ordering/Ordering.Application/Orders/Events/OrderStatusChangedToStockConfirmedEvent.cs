namespace Ordering.Application.Orders.IntegrationEvents;

public record OrderStatusChangedToStockConfirmedEvent (Guid OrderId, string status): IntegrationEvent;
