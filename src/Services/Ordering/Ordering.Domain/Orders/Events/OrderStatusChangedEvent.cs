using Ordering.Domain.Orders.Enums;
using Ordering.Domain.Orders.Models;

namespace Ordering.Domain.Orders.Events;

public record OrderStatusChangedEvent(Order order, OrderStatus status) : IDomainEvent;
