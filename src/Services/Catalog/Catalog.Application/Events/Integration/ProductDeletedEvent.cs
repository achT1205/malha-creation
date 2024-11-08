namespace Catalog.Application.Events.Integration;
public record ProductDeletedEvent : IntegrationEvent
{
    public Guid ProductId { get; set; } 
}
