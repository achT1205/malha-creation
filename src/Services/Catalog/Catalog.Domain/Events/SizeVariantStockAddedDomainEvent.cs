namespace Catalog.Domain.Events;

public record SizeVariantStockAddedDomainEvent(Guid colorVariantId, Guid SizeVariantId, int CurrentStock) :IDomainEvent;
