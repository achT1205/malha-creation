namespace Catalog.API.Models;
public class Product 
{
    public Guid Id { get; set; }  // Identifiant unique du produit
    public string Name { get; set; } = default!; // Nom du produit
    public string NameEn { get; set; } = default!; // Nom du produit
    public string CoverImage { get; set; } = default!; // Image de couverture
    public string ProductType { get; set; } = default!;// Type de produit (Clothing, Accessory)
    public string ForOccasion { get; set; } = default!; // Occasion (e.g., casual, formal)
    public string Description { get; set; } = default!; // Description détaillée
    public string Material { get; set; } = default!; // Matériau (e.g., coton, cuir, métal)
    public bool IsHandmade { get; set; }  // Indique si le produit est fait main
    public DateTime CreatedAt { get; set; }  // Date de création
    public DateTime UpdatedAt { get; set; }  // Date de mise à jour
    public string Collection { get; set; } = default!; // Collection associée
    public List<string> Categories { get; set; } = new(); // Liste des catégories
    public List<string> Colors { get; set; } = new(); // Liste des couleurs
    public List<ColorVariant> ColorVariants { get; set; } = new();  // Redéfinit les variantes pour avoir des prix directs
}


public class ColorVariant 
{
    public string Color { get; set; } = default!; // Nom ou identifiant de la couleur
    public List<string> Images { get; set; } = new(); // Liste des images supplémentaires
    public string Slug { get; set; } = default!; // Identifiant "slug" pour l'URL
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