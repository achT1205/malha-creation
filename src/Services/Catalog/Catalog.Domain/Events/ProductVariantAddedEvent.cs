namespace Catalog.Domain.Events;

public record ProductVariantAddedEvent(ColorVariant ColorVariant) : IDomainEvent;