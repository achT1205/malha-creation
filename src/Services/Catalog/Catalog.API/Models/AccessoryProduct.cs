namespace Catalog.API.Models;
// Produit spécifique pour les accessoires (pas de tailles, juste couleurs)
public class AccessoryProduct : ProductBase
{
    public List<ColorVariant> ColorVariants { get; set; } = new();  // Redéfinit les variantes pour avoir des prix directs
}
