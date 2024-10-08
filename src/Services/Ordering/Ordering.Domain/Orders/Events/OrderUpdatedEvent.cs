using Ordering.Domain.Orders.Models;

namespace Ordering.Domain.Orders.Events;

public record OrderUpdatedEvent(Order order) : IDomainEvent;