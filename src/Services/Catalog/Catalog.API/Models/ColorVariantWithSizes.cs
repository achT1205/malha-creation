namespace Catalog.API.Models;
// Variante de couleur spécifique pour les vêtements (avec tailles)
public class ColorVariantWithSizes : ColorVariantBase
{
    public List<SizeVariant> Sizes { get; set; } = new(); // Liste des tailles disponibles pour cette couleur
}
