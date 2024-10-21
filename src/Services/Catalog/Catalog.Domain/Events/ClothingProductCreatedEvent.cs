namespace Catalog.Domain.Events;

public record ClothingProductCreatedEvent(ClothingProduct product) : IDomainEvent;