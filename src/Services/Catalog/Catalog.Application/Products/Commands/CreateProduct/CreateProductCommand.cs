using BuildingBlocks.CQRS;

namespace Catalog.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(
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
    List<ColorVariantDto> ColorVariants
   )
    : ICommand<CreateProductResult>;

public class ImageDto
{
    public string ImageSrc { get; private set; } = default!;
    public string AltText { get; private set; } = default!;
}

public class ColorVariantDto
{
    public string Color { get; set; } = default!;
    public List<ImageDto> Images { get; set; } = new();
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public List<SizeVariantDto>  sizeVariants { get; set; } = new();
}

public class SizeVariantDto
{
    public string Size { get; set; } = default!;
    public decimal Price { get; set; }
    public decimal Quantity { get; set; }
}

