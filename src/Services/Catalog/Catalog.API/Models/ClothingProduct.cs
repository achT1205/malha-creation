namespace Catalog.API.Models;
// Produit spécifique pour les vêtements (avec tailles)
public class ClothingProduct : ProductBase
{
    public new List<ColorVariantWithSizes> ColorVariants { get; set; }  // Redéfinit les variantes pour inclure les tailles
}
