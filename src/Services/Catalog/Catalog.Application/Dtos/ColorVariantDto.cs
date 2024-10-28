namespace Catalog.Application.Dtos;
public class ColorVariantDto
{
    public string Color { get; set; } = default!;
    public List<ImageDto> Images { get; set; } = new();
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public List<SizeVariantDto> sizeVariants { get; set; } = new();
}


public record OutputColorVariantDto(
        string Color,
        List<ImageDto> Images,
        PriceDto? Price,
        int? Quantity,
        List<SizeVariantDto>? SizeVariants
    );