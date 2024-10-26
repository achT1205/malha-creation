﻿using BuildingBlocks.CQRS;
using BuildingBlocks.Enums;
using Catalog.Application.Dtos;

namespace Catalog.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string UrlFriendlyName,
    string Description,
    bool IsHandmade,
    ImageDto CoverImage,
    Guid ProductTypeId,
    ProductTypeEnum ProductType,
    Guid MaterialId,
    Guid CollectionId,
    List<Guid> OccasionIds,
    List<Guid> CategoryIds,
    List<ColorVariantDto> ColorVariants
   )
    : ICommand<CreateProductResult>;