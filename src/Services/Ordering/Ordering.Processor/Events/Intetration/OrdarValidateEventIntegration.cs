using BuildingBlocks.Messaging.Events;

namespace Ordering.Processor.Events.Intetration;

public record OrdarValidateEventIntegration(Guid OrderId) : IntegrationEvent;