using BuildingBlocks.Messaging.Events;

namespace PaymentProcessor.Events.IntegrationEvents;
public record OrderPaymentFailedIntegrationEvent(Guid OrderId) : IntegrationEvent;
