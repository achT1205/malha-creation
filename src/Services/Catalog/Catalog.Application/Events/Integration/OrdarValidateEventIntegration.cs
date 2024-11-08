using BuildingBlocks.Messaging.Events;

namespace Catalog.Application.Events.Integration;
public record OrdarValidateEventIntegration(Guid OrderId) : IntegrationEvent;