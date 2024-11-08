using BuildingBlocks.Messaging.Events;

namespace Ordering.Application.Orders.Events;
public record OrderPaymentFailedIntegrationEvent(Guid OrderId) : IntegrationEvent;
