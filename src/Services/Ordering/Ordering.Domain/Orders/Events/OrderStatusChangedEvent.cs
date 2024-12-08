using Ordering.Domain.Orders.Models;

namespace Ordering.Domain.Orders.Events;

public record OrderStatusChangedEvent(Order order, OrderStatus status) : IDomainEvent;
