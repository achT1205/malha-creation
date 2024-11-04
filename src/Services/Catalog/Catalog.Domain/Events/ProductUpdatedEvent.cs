namespace Catalog.Domain.Events;
public record ProductUpdatedEvent(Product product) : IDomainEvent;