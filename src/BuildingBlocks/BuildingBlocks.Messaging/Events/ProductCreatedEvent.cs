namespace BuildingBlocks.Messaging.Events;

public record ProductCreatedEvent : IntegrationEvent
{
    public new Guid Id { get; set; }  // Identifiant unique du produit
    public string Name { get; set; } = default!; // Nom du produit
    public string ProductType { get; set; } = default!;// Type de produit (Clothing, Accessory)
    public List<ColorItem> ColorVariants { get; set; } = new();
}

public class ColorItem
{
    public decimal? Price { get; set; }  // Prix pour cette couleur
    public int? Quantity { get; set; }  // Quantité disponible pour cette couleur
    public string Color { get; set; } = default!; // Nom ou identifiant de la couleur
    public string CoverImage { get; set; } = default!; // URL de l'image de couverture pour cette couleur
    public List<string> Images { get; set; } = new(); // Liste des images supplémentaires
    public string Slug { get; set; } = default!; // Identifiant "slug" pour l'URL
    public List<SizeItem>? Sizes { get; set; } = new(); // Liste des tailles disponibles pour cette couleur
}
public class SizeItem
{
    public string Size { get; set; } = default!; // Taille (e.g., S, M, L)
    public decimal Price { get; set; }  // Prix pour cette taille
    public int Quantity { get; set; }  // Quantité disponible pour cette taille
}