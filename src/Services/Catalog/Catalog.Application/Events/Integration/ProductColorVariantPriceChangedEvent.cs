namespace Catalog.Application.Events.Integration;

public record ProductColorVariantPriceChangedEvent(Guid Id, Guid ColorVariantId) : IntegrationEvent;
