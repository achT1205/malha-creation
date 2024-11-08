namespace Catalog.Application.Events.Integration;

public record OrderStockConfirmedIntegrationEvent(Guid OrderId) : IntegrationEvent;
