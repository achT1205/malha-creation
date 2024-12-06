﻿namespace Catalog.Application.Dtos;
public class ColorVariantDto
{
    public Guid? Id { get; set; }
    public string Color { get; set; } = default!;
    public List<ImageDto> Images { get; set; } = new();
    public decimal? Price { get; set; }
    public int? Quantity { get; set; }
    public List<SizeVariantDto>? sizeVariants { get; set; } = new();
    public int? RestockThreshold { get; set; }
    public List<Guid>? OutfitIds { get; set; } = new();
}


public record OutputColorVariantDto(
    Guid Id,
    string Color,
    string Background,
    List<ImageDto> Images,
    PriceDto Price,
    int? Quantity,
    int? RestockThreshold,
    string Slug,
    List<SizeVariantDto> SizeVariants,
    List<Guid>? OutfitIds
    );


public record StockColorVariantDto(
        Guid Id,
        string Color,
        int? Quantity,
        List<StockSizeVariantDto> SizeVariants
    );