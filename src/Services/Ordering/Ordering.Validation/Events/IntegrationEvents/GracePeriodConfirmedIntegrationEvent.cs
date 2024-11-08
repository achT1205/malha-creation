using BuildingBlocks.Messaging.Events;

namespace Ordering.Validation.Events.IntegrationEvents;

public record OrdarValidateEventIntegration (Guid OrderId) : IntegrationEvent;
