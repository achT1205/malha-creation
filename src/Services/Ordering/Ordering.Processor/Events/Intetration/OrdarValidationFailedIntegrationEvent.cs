using BuildingBlocks.Messaging.Events;

namespace Ordering.Processor.Events.Intetration;
public record OrdarValidationFailedIntegrationEvent(Guid OrderId) : IntegrationEvent;