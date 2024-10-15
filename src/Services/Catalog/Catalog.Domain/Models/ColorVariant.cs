using Catalog.Domain.Events;

namespace Catalog.Domain.Models;

public class ColorVariant : Aggregate<ColorVariantId>
{
    private readonly List<SizeVariant> _sizeVariants = new();
    public IReadOnlyList<SizeVariant> SizeVariants => _sizeVariants.AsReadOnly();

    internal ColorVariant(ProductId productId, string color, List<string> images, string slug, Price? price)
    {
        Id = ColorVariantId.Of(Guid.NewGuid());
        ProductId = productId;
        Color = color;
        Images = images;
        Slug = slug;
        Price = price;
    }
    public ProductId ProductId { get; private set; } = default!;
    public string Color { get; private set; } = default!;
    public List<string> Images { get; private set; } = new();
    public string Slug { get; private set; } = default!;
    public Price? Price { get; private set; }


    public void AddSizeVariant(string size, decimal price, int quantity)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(size);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
        var sizeVariant = new SizeVariant(Id, Size.Of(size), Price.Of(price), quantity);
        _sizeVariants.Add(sizeVariant);
        AddDomainEvent(new ProductSizeVariantAddedEvent(sizeVariant));
    }

    public void RemoveSizeVariant(SizeVariantId sizeVariantId)
    {
        var sizeVariant = _sizeVariants.FirstOrDefault(x => x.Id == sizeVariantId);
        if (sizeVariant is not null)
        {
            _sizeVariants.Remove(sizeVariant);
            AddDomainEvent(new ProductSizeVariantRemovedEvent(sizeVariant));
        }
    }

}