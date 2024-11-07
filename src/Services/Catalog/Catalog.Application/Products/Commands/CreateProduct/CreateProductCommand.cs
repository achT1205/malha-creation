using BuildingBlocks.CQRS;
using Catalog.Application.Dtos;
using Catalog.Domain.Enums;

namespace Catalog.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string UrlFriendlyName,
    string Description,
    bool IsHandmade,
    bool OnReorder,
    ImageDto CoverImage,
    Guid ProductTypeId,
    ProductTypeEnum ProductType,
    Guid MaterialId,
    Guid BrandId,
    Guid CollectionId,
    List<Guid> OccasionIds,
    List<Guid> CategoryIds,
    List<ColorVariantDto> ColorVariants
   )
    : ICommand<CreateProductResult>;