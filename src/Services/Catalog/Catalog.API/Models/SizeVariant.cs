namespace Catalog.API.Models;

public class SizeVariant
{
    public string Size { get; set; } = default!; // Taille (e.g., S, M, L)
    public decimal Price { get; set; }  // Prix pour cette taille
    public int Quantity { get; set; }  // Quantité disponible pour cette taille
}