using BuildingBlocks.Messaging.Events;

namespace Catalog.Application.Events.IntegrationEvents;

public record ProductUpdatedEvent : IntegrationEvent
{
    public Guid Id { get; set; }  
    public string ProductType { get; set; } = default!;
    public List<ColorItem> ColorVariants { get; set; } = new();
}