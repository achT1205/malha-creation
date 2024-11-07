namespace Catalog.Domain.Events;

public record ProductColorVariantPriceChangedDomainEvent(Guid ProductId, Guid ColorVariantId, decimal Price, decimal OldPrice, string Currency) : IDomainEvent;