using BuildingBlocks.Messaging.Events;

namespace Ordering.Processor.Events.Intetration;

public record OrdarValidationSucceededIntegrationEvent(Guid OrderId) : IntegrationEvent;