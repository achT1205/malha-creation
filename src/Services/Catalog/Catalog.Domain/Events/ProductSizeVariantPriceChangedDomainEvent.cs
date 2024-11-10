namespace Catalog.Domain.Events;

public record ProductSizeVariantPriceChangedDomainEvent(Guid colorVariantId, Guid sizeVariantId, decimal colorVariantPrice) : IDomainEvent;
