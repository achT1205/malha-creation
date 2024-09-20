namespace Catalog.API.Dtos;
public class CreateProductDto
{
    public string Name { get; set; } = default!; // Nom du produit
    public string NameEn { get; set; } = default!; // Nom du produit
    public string CoverImage { get; set; } = default!; // Image de couverture
    public string ProductType { get; set; } = default!;// Type de produit (Clothing, Accessory)
    public string ForOccasion { get; set; } = default!; // Occasion (e.g., casual, formal)
    public string Description { get; set; } = default!; // Description détaillée
    public string Material { get; set; } = default!; // Matériau (e.g., coton, cuir, métal)
    public bool IsHandmade { get; set; }  // Indique si le produit est fait main
    public DateTime CreatedAt { get; set; }  // Date de création
    public string Collection { get; set; } = default!; // Collection associée
    public List<string> Categories { get; set; } = new(); // Liste des catégories
    public List<ColorVariantDto> ColorVariants { get; set; } = new();
}

public class ColorVariantDto
{
    public string Color { get; set; } = default!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public List<string> Images { get; set; } = new();
    public List<SizeVariantDto> Sizes { get; set; } = new();
}

public class SizeVariantDto
{
    public string Size { get; set; } = default!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
