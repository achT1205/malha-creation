namespace Catalog.Domain.Models;

public abstract class ColorVariant : Entity<ColorVariantId>
{
    public ProductId ProductId { get; protected set; } = default!;
    public Color Color { get; protected set; } = default!;
    public Slug Slug { get; protected set; } = default!;

    protected readonly List<Image> _images = new();
    public IReadOnlyList<Image> Images => _images.AsReadOnly();

    protected ColorVariant()
    {

    }
    protected ColorVariant(ProductId productId, Color color, Slug slug)
    {
        Id = ColorVariantId.Of(Guid.NewGuid());
        ProductId = productId;
        Color = color ?? throw new ArgumentNullException(nameof(color));
        Slug = slug ?? throw new ArgumentNullException(nameof(slug));
    }

    public abstract void AddImage(Image image);
}
