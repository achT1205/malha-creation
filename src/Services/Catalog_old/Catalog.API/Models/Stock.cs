namespace Catalog.API.Models;

public class Stock
{
    public Guid ProductId { get; set; }  // Clé étrangère vers la table des produits
    public List<StokColorVariant> ColorVariants { get; set; } = new();  // Redéfinit les variantes pour avoir des prix directs

}
public class StokColorVariant
{
    public string Color { get; set; } = default!; // Nom ou identifiant de la couleur
    public int? Quantity { get; set; }  // Quantité disponible pour cette couleur
    public string? Size { get; set; } = default!; // Taille (e.g., S, M, L)
    public int LowStockThreshold { get; set; } = 20;  // Seuil de stock bas pour alerte
}