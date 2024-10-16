namespace Catalog.Domain.Models;

public abstract class ColorVariantBase : Entity<ColorVariantId>
{
    public Color Color { get; protected set; }
    public Slug Slug { get; protected set; }
    public List<Image> Images { get; protected set; }

    protected ColorVariantBase(ColorVariantId id, Color color, Slug slug, List<Image> images)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Color = color ?? throw new ArgumentNullException(nameof(color));
        Slug = slug ?? throw new ArgumentNullException(nameof(slug));
        Images = images ?? new List<Image>();
    }

    public abstract void AddSizeVariant(SizeVariant sizeVariant);
    public abstract IReadOnlyCollection<SizeVariant> GetSizes();
}
