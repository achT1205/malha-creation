using Ordering.Domain.Orders.Models;

namespace Ordering.Domain.Orders.Events;

public record GracePeriodConfirmedEvent(Order order) : IDomainEvent;