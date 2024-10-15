
namespace Catalog.Domain.Events;

public record ProductCreatedEvent(Product product) : IDomainEvent;