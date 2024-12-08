namespace Catalog.Domain.Events;

public record ProductColorVariantRemovedDomainEvent(ColorVariant ColorVariant) : IDomainEvent;