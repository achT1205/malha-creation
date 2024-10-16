namespace Catalog.Application.Products.Dtos;

// DTO pour représenter un produit
public record ProductDto(
    Guid Id,
    string Name,
    string Description,
    bool IsHandmade,
    string ProductType,
    string Material,
    string Collection,
    List<CategoryDto> Categories,
    List<OccasionDto> Occasions,
    List<ColorVariantDto> ColorVariants);

// DTO pour représenter une catégorie
public record CategoryDto(Guid Id, string Name);

// DTO pour représenter une occasion
public record OccasionDto(Guid Id, string Name);

// DTO pour représenter une variante de couleur
public record ColorVariantDto(
    Guid Id,
    string Color,
    string Slug,
    List<string> ImageUrls,
    List<SizeVariantDto>? SizeVariants, // Nullable for accessories
    decimal? Price, // Nullable for clothing
    int? Quantity); // Nullable for clothing

// DTO pour représenter une variante de taille (pour les vêtements)
public record SizeVariantDto(string Size, decimal Price, int Quantity);
