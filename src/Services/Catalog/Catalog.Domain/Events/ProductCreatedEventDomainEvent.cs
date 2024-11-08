
namespace Catalog.Domain.Events;

public record ProductCreatedEventDomainEvent(Product product) : IDomainEvent;