using BuildingBlocks.Messaging.Events;

namespace Catalog.Application.Events.IntegrationEvents;

public record ProductColorVariantPriceChangedEvent(Guid Id, Guid ColorVariantId) : IntegrationEvent;
