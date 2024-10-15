namespace Catalog.Domain.Events;
public record ProductVariantRemovedEvent(ColorVariant ColorVariant) : IDomainEvent;