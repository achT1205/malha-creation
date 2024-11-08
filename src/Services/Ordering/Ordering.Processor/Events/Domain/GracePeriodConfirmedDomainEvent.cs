using Ordering.Domain.Abstractions;

namespace Ordering.Processor.Events.Domain;

public record GracePeriodConfirmedDomainEvent(Guid OrderId) : IDomainEvent;
