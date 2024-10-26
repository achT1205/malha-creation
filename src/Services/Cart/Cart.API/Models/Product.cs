using BuildingBlocks.Enums;

namespace Cart.API.Models;

public class Product
{
    public Guid Id { get; set; }  // Identifiant unique du produit
    public string Name { get; set; } = default!; // Nom du produit
    public ProductTypeEnum ProductType { get; set; } // Type de produit (Clothing, Accessory)
    public List<ColorVariant> ColorVariants { get; set; } = new();  // Redéfinit les variantes pour avoir des prix directs
}


public class ColorVariant
{
    public string Slug { get; set; } = default!; // Identifiant "slug" pour l'URL
    public string Color { get; set; } = default!; // Nom ou identifiant de la couleur
    public decimal? Price { get; set; }  // Prix pour cette couleur
    public int? Quantity { get; set; }  // Quantité disponible pour cette couleur
    public List<SizeVariant>? Sizes { get; set; } = new(); // Liste des tailles disponibles pour cette couleur
}

public class SizeVariant
{
    public string Size { get; set; } = default!; // Taille (e.g., S, M, L)
    public decimal Price { get; set; }  // Prix pour cette taille
    public int Quantity { get; set; }  // Quantité disponible pour cette taille
}