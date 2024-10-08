using Ordering.Domain.Orders.Models;

namespace Ordering.Domain.Orders.Events;

public record OrderCreatedEvent(Order order) : IDomainEvent;