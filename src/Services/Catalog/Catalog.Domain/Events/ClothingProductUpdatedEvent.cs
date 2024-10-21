namespace Catalog.Domain.Events;

public record ClothingProductUpdatedEvent(ClothingProduct product) : IDomainEvent;