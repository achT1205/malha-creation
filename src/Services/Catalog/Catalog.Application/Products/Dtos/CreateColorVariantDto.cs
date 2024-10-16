
public abstract record CreateColorVariantDto(
    string Color,
    List<Guid> ImageIds);

// Record pour les variantes d'accessoires (avec Prix et Quantité)
public record CreateAccessoryColorVariantDto(
    string Color,
    List<Guid> ImageIds,
    decimal Price,
    int Quantity)
    : CreateColorVariantDto(Color, ImageIds);

// Record pour les variantes de vêtements (avec variantes de taille)
public record CreateClothingColorVariantDto(
    string Color,
    List<Guid> ImageIds,
    List<CreateSizeVariantDto> SizeVariants)
    : CreateColorVariantDto(Color, ImageIds);

// DTO pour créer les variantes de taille
public record CreateSizeVariantDto(
    string Size,
    decimal Price,
    int Quantity);
