namespace Catalog.Domain.Events;

public record AccessoryProductCreatedEvent(AccessoryProduct product) : IDomainEvent;