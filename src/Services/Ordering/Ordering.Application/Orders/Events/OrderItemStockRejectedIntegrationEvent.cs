using BuildingBlocks.Messaging.Events;

namespace Ordering.Application.Orders.Events;
public record OrderItemStockRejectedIntegrationEvent(OrderStockRejectedDto OrderStockRejected) : IntegrationEvent;
