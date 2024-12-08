namespace Catalog.Domain.Models;

public class ColorVariant : Entity<ColorVariantId>
{
    public ProductId ProductId { get; private set; } = default!;
    public Color Color { get; protected set; } = default!;
    public Slug Slug { get; protected set; } = default!;
    public bool OnOrdering { get; private set; } = default!;
    public IReadOnlyList<ColorVariantId> Outfits => _outfits.AsReadOnly();

    private readonly List<ColorVariantId> _outfits = new();
    public ColorVariantQuantity RestockThreshold { get; private set; } = default!;

    private readonly List<Image> _images = new();
    public IReadOnlyList<Image> Images => _images.AsReadOnly();

    private readonly List<SizeVariant> _sizeVariants = new();
    public IReadOnlyList<SizeVariant> SizeVariants => _sizeVariants.AsReadOnly();

    public ColorVariantPrice Price { get; private set; } = default!;
    public ColorVariantQuantity Quantity { get; private set; } = default!;

    private ColorVariant()
    {

    }
    private ColorVariant(ProductId productId, Color color, Slug slug, ColorVariantPrice price, ColorVariantQuantity quantity, ColorVariantQuantity restockThreshold)
    {
        Id = ColorVariantId.Of(Guid.NewGuid());
        Price = price;
        Quantity = quantity;
        ProductId = productId;
        Color = color ?? throw new ArgumentNullException(nameof(color));
        Slug = slug ?? throw new ArgumentNullException(nameof(slug));
        RestockThreshold = restockThreshold;
    }

    public static ColorVariant Create(ProductId productId, Color color, Slug slug, ColorVariantPrice price, ColorVariantQuantity quantity, ColorVariantQuantity restockThreshold)
    {
        return new ColorVariant(productId, color, slug, price, quantity, restockThreshold);
    }

    public void Update(Color newColor, Slug newSlug, ColorVariantPrice newPrice, ColorVariantQuantity newQuantity, ColorVariantQuantity newRestockThreshold)
    {
        if (!Color.Equals(newColor))
        {
            Color = newColor ?? throw new ArgumentNullException(nameof(newColor));
        }
        if (!Slug.Equals(newSlug))
        {
            Slug = newSlug ?? throw new ArgumentNullException(nameof(newSlug));
        }
        if (!Price.Equals(newPrice))
        {
            Price = newPrice ?? throw new ArgumentNullException(nameof(newPrice));
        }
        if (!Quantity.Equals(newQuantity))
        {
            Quantity = newQuantity ?? throw new ArgumentNullException(nameof(newQuantity));
        }
        if (!RestockThreshold.Equals(newRestockThreshold))
        {
            RestockThreshold = newRestockThreshold ?? throw new ArgumentNullException(nameof(newRestockThreshold));
        }
    }

    public void AddStock(int newQuantity)
    {

        if (newQuantity <= 0)
        {
            throw new CatalogDomainException($"Item quantity should be greater than zero");
        }
        Quantity = Quantity.Increase(newQuantity);
    }
    public void RemoveStock(int newQuantity)
    {
        if (Quantity.Equals(0))
        {
            throw new CatalogDomainException($"Empty stock, product item {Slug.Value} is sold out");
        }

        if (newQuantity <= 0)
        {
            throw new CatalogDomainException($"Item units desired should be greater than zero");
        }
        Quantity = Quantity.Decrease(newQuantity);
    }
    public void AddSizeVariant(SizeVariant sizeVariant)
    {
        if (sizeVariant == null)
        {
            throw new ArgumentNullException(nameof(sizeVariant));
        }

        if (_sizeVariants.Any(cv => cv.Size.Value.ToLower() == sizeVariant.Size.Value.ToLower()))
            throw new InvalidOperationException($"A Size Variant with the same Name \"{sizeVariant.Size.Value}\"  already exists.");
        _sizeVariants.Add(sizeVariant);
    }
    public void AddOutfit(ColorVariantId colorVariantId)
    {
        if (colorVariantId == null)
        {
            throw new ArgumentNullException(nameof(colorVariantId));
        }

        if (_outfits.Any(id => id == colorVariantId))
            throw new InvalidOperationException($"\"{colorVariantId}\"  already exists.");
        _outfits.Add(colorVariantId);
    }
    public void RemoveOutfit(ColorVariantId colorVariantId)
    {
        var size = _outfits.FirstOrDefault(_ => _ == colorVariantId);
        if (size != null)
        {
            _outfits.Remove(colorVariantId);
        }
    }
    public void RemoveSizeVariant(SizeVariantId sizeVariantId)
    {
        var size = _sizeVariants.FirstOrDefault(_ => _.Id.Value == sizeVariantId.Value);
        if (size != null)
        {
            if (size.OnOrdering)
            {
                throw new CatalogDomainException($"This sizeVariant is on ordering, can not remove it.");
            }
            _sizeVariants.Remove(size);
        }
    }
    public void AddImage(Image image)
    {
        if (_images.Contains(image))
            return;

        _images.Add(image);
    }
    public void RemoveImage(string src)
    {
        var image = _images.FirstOrDefault(_ => _.ImageSrc.ToLower() == src.ToLower());
        if (image != null)
        {
            _images.Remove(image);
        }
    }
}
