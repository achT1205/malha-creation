namespace Catalog.API.Models;
// Produit spécifique pour les vêtements (avec tailles)
public class ClothingProduct : ProductBase
{
    public List<ColorVariant> ColorVariants { get; set; } = new(); // Redéfinit les variantes pour inclure les tailles
}
