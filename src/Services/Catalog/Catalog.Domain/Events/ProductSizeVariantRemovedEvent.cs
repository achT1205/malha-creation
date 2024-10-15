namespace Catalog.Domain.Events;
public record ProductSizeVariantRemovedEvent(SizeVariant SizeVariant) : IDomainEvent;