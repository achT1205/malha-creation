namespace Catalog.Domain.Events;
public record ProductSizeVariantAddedEvent(SizeVariant SizeVariant) : IDomainEvent;