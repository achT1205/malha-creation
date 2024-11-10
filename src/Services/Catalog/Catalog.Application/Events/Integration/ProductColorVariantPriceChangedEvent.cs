namespace Catalog.Application.Events.Integration;

public record ProductColorVariantPriceChangedEvent(Guid ProductId, Guid ColorVariantId, decimal Price, decimal OldPrice, string Currency) : IntegrationEvent;
