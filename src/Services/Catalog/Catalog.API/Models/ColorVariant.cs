namespace Catalog.API.Models;
// Variante de couleur spécifique pour les vêtements (avec tailles)
public class ColorVariant : ColorVariantBase
{
    public decimal? Price { get; set; }  // Prix pour cette couleur
    public int? Quantity { get; set; }  // Quantité disponible pour cette couleur
    public List<SizeVariant>? Sizes { get; set; } = new(); // Liste des tailles disponibles pour cette couleur
}
