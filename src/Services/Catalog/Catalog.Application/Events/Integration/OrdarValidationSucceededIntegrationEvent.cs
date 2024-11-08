using BuildingBlocks.Messaging.Events;

namespace Catalog.Application.Events.Integration;
public record OrdarValidationSucceededIntegrationEvent(Guid OrderId) : IntegrationEvent;