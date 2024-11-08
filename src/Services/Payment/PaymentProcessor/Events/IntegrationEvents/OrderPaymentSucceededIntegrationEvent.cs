using BuildingBlocks.Messaging.Events;

namespace PaymentProcessor.Events.IntegrationEvents;
public record OrderPaymentSucceededIntegrationEvent(Guid OrderId) : IntegrationEvent;
