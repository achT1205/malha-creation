namespace Catalog.Application.Dtos;

public record ProductDto
(
    Guid Id,
    string Name,
    string UrlFriendlyName,
    string Description,
    bool IsHandmade,
    ProductType ProductType,
    ImageDto CoverImage,
    MaterialDto Material,
    CollectionDto Collection,
    BrandDto Brand,
    List<OutputColorVariantDto> ColorVariants,
    List<OccasionDto>? Occasions,
    List<CategoryDto>? Categories
);

public record LiteProductDto
(
    Guid Id,
    string Name,
    string UrlFriendlyName,
    string Description,
    bool IsHandmade,
    ProductType ProductType,
    ImageDto CoverImage,
    Guid MaterialId,
    Guid CollectionId,
    Guid BrandId,
    List<OutputColorVariantDto> ColorVariants,
    List<Guid>? OccasionIds,
    List<Guid>? CategoryIds
);



public record ProductStockDto
(
    Guid Id,
    ProductType ProductType,
    List<StockColorVariantDto> ColorVariants
);

public record ProductTypeDto(Guid Id, string Name);
public record MaterialDto(Guid Id, string Name, string Description);
public record BrandDto(Guid Id, string Name, string Description, string WebsiteUrl, ImageDto Logo);
public record OccasionDto(Guid Id, string Name, string Description);
public record CategoryDto(Guid Id, string Name, string Description, ImageDto CoverImage);
public record CollectionDto(Guid Id, string Name, string Description, ImageDto CoverImage);