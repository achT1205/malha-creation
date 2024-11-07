namespace Catalog.Domain.Events;

public record ProductNewColorVariantAddedDomainEvent(ColorVariant ColorVariant) : IDomainEvent;