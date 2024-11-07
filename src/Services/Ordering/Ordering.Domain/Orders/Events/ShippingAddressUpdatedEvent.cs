using Ordering.Domain.Orders.Models;

namespace Ordering.Domain.Orders.Events;

public record ShippingAddressUpdatedEvent(Order order) : IDomainEvent;