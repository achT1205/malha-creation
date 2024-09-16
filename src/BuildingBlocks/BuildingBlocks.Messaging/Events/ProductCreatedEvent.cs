namespace BuildingBlocks.Messaging.Events;

public record ProductCreatedEvent : IntegrationEvent
{
    public new Guid Id { get; set; }  // Identifiant unique du produit
    public string Name { get; set; } = default!; // Nom du produit
    public string ProductType { get; set; } = default!;// Type de produit (Clothing, Accessory)
    public List<string> Colors { get; set; } = new(); // Liste des couleurs
    public List<string> Sizes { get; set; } = new(); // Liste des tailles

}