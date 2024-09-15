namespace Catalog.API.Models;
public class ColorVariantWithPrice : ColorVariantBase
{
    public decimal Price { get; set; }  // Prix pour cette couleur
    public int Quantity { get; set; }  // Quantité disponible pour cette couleur
}