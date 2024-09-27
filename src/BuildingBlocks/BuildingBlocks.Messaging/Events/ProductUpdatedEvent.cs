
namespace BuildingBlocks.Messaging.Events;

public record ProductUpdatedEvent : IntegrationEvent
{
    public Guid Id { get; set; }  // Identifiant unique du produit
    public string ProductType { get; set; } = default!;// Type de produit (Clothing, Accessory)
    public List<ColorItem> ColorVariants { get; set; } = new();
}