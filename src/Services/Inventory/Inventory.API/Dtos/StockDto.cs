namespace Inventory.API.Dtos;

public class StockDto
{
    public Guid ProductId { get; set; }  // Clé étrangère vers la table des produits
    public string ProductType { get; set; } = default!;// Type de produit (Clothing, Accessory)
    public List<ColorVariant> ColorVariants { get; set; } = new();  // Redéfinit les variantes pour avoir des prix directs
}