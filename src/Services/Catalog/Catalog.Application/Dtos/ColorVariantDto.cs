namespace Catalog.Application.Dtos;
public class ColorVariantDto
{
    public Guid? Id { get; set; }
    public string Color { get; set; } = default!;
    public List<ImageDto> Images { get; set; } = new();
    public decimal? Price { get; set; }
    public int? Quantity { get; set; }
    public List<SizeVariantDto>? SizeVariants { get; set; } = new();
    public int? RestockThreshold { get; set; }
    public List<Guid>? OutfitIds { get; set; } = new();
    public string Currency { get; set; } = default!;
}


public record OutputColorVariantDto(
    Guid Id,
    string Color,
    string Background,
    string Class,
    List<ImageDto> Images,
    decimal? Price,
    int? Quantity,
    int? RestockThreshold,
    string Slug,
    List<SizeVariantDto> SizeVariants,
    List<Guid>? OutfitIds);


public record StockColorVariantDto(
        Guid Id,
        string Color,
        int? Quantity,
        List<StockSizeVariantDto> SizeVariants
    );