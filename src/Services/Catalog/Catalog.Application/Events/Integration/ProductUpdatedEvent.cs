using BuildingBlocks.Messaging.Events;

namespace Catalog.Application.Events.Integration;

public record ProductUpdatedEvent : IntegrationEvent
{
    public Guid Id { get; set; }  
    public string ProductType { get; set; } = default!;
    public List<ColorItem> ColorVariants { get; set; } = new();
}