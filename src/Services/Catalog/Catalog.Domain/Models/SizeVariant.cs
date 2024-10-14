using Catalog.Domain.Abstractions;
using Catalog.Domain.ValueObjects;

namespace Catalog.Domain.Models;

public class SizeVariant : Entity<SizeVariantId>
{
    public Size Size { get; private set; } = default!;
    public Price Price { get; private set; }

    public SizeVariant(SizeVariantId id, Size size, Price price, int quantity)
    {
        Id = id;
        Size = size;
        Price = price;
    }
}
