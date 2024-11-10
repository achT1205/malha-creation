using Catalog.Application.Brands.Queries;
using Catalog.Application.Categories.Queries;
using Catalog.Application.Collections.Queries;
using Catalog.Application.Materials.Queries;
using Catalog.Application.Occasions.Queries;
using Catalog.Application.ProductTypes.Queries;

namespace Catalog.Application.Dtos;

public record ProductDto
(
    Guid Id,
    string Name,
    string UrlFriendlyName,
    string Description,
    bool IsHandmade,
    ImageDto CoverImage,
    ProductTypeDto ProductType,
    MaterialDto Material,
    CollectionDto Collection,
    BrandDto Brand,
    List<OutputColorVariantDto> ColorVariants,
    List<OccasionDto>?  Occasions,
    List<CategoryDto>? Categories
);


public record ProductStockDto
(
    Guid Id,
    ProductTypeEnum ProductType, 
    List<StockColorVariantDto> ColorVariants
);

public record ProductTypeDto(Guid Id, string Name);
public record MaterialDto(Guid Id, string Name);
public record BrandDto(Guid Id, string Name);
public record OccasionDto(Guid Id, string Name);
public record CategoryDto(Guid Id, string Name);
public record CollectionDto(
Guid Id,
    string Name,
    string ImageSrc,
    string AltText
    );