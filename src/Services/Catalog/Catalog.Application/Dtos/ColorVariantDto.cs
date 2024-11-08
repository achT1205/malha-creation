namespace Catalog.Application.Dtos;
public class ColorVariantDto
{
    public string Color { get; set; } = default!;
    public List<ImageDto> Images { get; set; } = new();
    public decimal ? Price { get; set; }
    public int ? Quantity { get; set; }
    public List<SizeVariantDto> ? sizeVariants { get; set; } = new();
    public int? RestockThreshold { get; internal set; }
}


public record OutputColorVariantDto(
        Guid Id,
        string Color,
        List<ImageDto> Images,
        PriceDto Price,
        int ? Quantity,
        string Slug,
        List<SizeVariantDto> SizeVariants
    );


public record StockColorVariantDto(
        Guid Id,
        string Color,
        int? Quantity,
        List<StockSizeVariantDto> SizeVariants
    );