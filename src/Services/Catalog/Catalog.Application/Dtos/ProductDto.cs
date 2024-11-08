using Catalog.Domain.Enums;

namespace Catalog.Application.Dtos;

public record ProductDto
(
    Guid Id,
    string Name,
    string UrlFriendlyName,
    string Description,
    bool IsHandmade,
    ImageDto CoverImage,
    Guid ProductTypeId,
    Guid MaterialId,
    Guid CollectionId,
    List<Guid> OccasionIds,
    List<Guid> CategoryIds,
    List<OutputColorVariantDto> ColorVariants,
    string? ProductType,
    string? Material,
    string? Collection,
    List<string>?  Occasions,
    List<string>? Categories
);


public record ProductStockDto
(
    Guid Id,
    ProductTypeEnum ProductType, 
    List<StockColorVariantDto> ColorVariants
);