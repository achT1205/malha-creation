namespace BuildingBlocks.Messaging.Events;
public record ProductDeletedEvent : IntegrationEvent
{
    public Guid ProductId { get; set; }  // Identifiant unique du produit
}
