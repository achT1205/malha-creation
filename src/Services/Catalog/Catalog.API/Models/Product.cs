namespace Catalog.API.Models;
// Classe de base pour tous les produits
public class Product : ProductBase
{
    public List<ColorVariant> ColorVariants { get; set; } = new();  // Redéfinit les variantes pour avoir des prix directs
}