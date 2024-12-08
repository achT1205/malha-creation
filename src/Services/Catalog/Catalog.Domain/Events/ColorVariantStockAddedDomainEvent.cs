namespace Catalog.Domain.Events;

public record ColorVariantStockAddedDomainEvent(Guid colorVariantId, int CurrentStock) :IDomainEvent;
