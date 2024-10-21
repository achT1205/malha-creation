namespace Catalog.Domain.Events;

public record AccessoryProductUpdatedEvent(AccessoryProduct product) : IDomainEvent;