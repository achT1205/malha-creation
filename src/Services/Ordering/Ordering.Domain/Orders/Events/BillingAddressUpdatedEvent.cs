using Ordering.Domain.Orders.Models;

namespace Ordering.Domain.Orders.Events;

public record BillingAddressUpdatedEvent(Order order) : IDomainEvent;