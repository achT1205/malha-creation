using BuildingBlocks.Messaging.Events;

namespace Catalog.Application.Events.Integration;

public record OrderItemStockRejectedIntegrationEvent(OrderStockRejectedDto OrderStockRejected) : IntegrationEvent;
