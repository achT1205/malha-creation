using BuildingBlocks.Messaging.Events;

namespace Catalog.Application.Events.IntegrationEvents;
public record ProductDeletedEvent : IntegrationEvent
{
    public Guid ProductId { get; set; } 
}
