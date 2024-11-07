using Ordering.Domain.Orders.Models;

namespace Ordering.Domain.Orders.Events;

public record PaymentUpdatedEvent(Order order) : IDomainEvent;