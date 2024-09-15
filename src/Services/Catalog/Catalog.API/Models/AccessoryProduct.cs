namespace Catalog.API.Models;
// Produit spécifique pour les accessoires (pas de tailles, juste couleurs)
public class AccessoryProduct //: ProductBase
{
    public new List<ColorVariantWithPrice> ColorVariants { get; set; } = new();  // Redéfinit les variantes pour avoir des prix directs
}
