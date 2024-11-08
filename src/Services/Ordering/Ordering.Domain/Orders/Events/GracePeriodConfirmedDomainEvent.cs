namespace Ordering.Domain.Orders.Events;

public record GracePeriodConfirmedDomainEvent(Guid OrderId) : IDomainEvent;