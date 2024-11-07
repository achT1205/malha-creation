using BuildingBlocks.Messaging.Events;

namespace PaymentProcessor.Events.IntegrationEvents;
public record OrderPaymentSucceeded(Guid OrderId) : IntegrationEvent;
