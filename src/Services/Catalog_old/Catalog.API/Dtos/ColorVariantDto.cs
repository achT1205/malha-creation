namespace Catalog.API.Dtos;

public class ColorVariantDto
{
    public string Color { get; set; } = default!;
    public decimal? Price { get; set; }
    public List<string> Images { get; set; } = new();
    public List<SizeVariantDto>? Sizes { get; set; } = new();
}
