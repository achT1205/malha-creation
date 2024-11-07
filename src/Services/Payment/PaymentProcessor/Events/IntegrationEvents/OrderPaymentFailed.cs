using BuildingBlocks.Messaging.Events;

namespace PaymentProcessor.Events.IntegrationEvents;
public record OrderPaymentFailed(Guid OrderId) : IntegrationEvent;
