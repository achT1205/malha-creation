namespace Inventory.API.Models;
public class Stock
{
    public Guid Id { get; set; } = Guid.NewGuid();  // Identifiant unique pour l'entrée de stock
    public Guid ProductId { get; set; }  // Clé étrangère vers la table des produits
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;// Date d'ajout de l'entrée de stock
    public DateTime UpdatedAt { get; set; }  // Date de dernière mise à jour
    public List<ColorVariant> ColorVariants { get; set; } = new();  // Redéfinit les variantes pour avoir des prix directs

}

public class ColorVariant
{
    public string Color { get; set; } = default!; // Nom ou identifiant de la couleur
    public int ? Quantity { get; set; }  // Quantité disponible pour cette couleur
    public string? Size { get; set; } = default!; // Taille (e.g., S, M, L)
    public int LowStockThreshold { get; set; } = 20;  // Seuil de stock bas pour alerte
}