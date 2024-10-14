using Catalog.Domain.Abstractions;
using Catalog.Domain.ValueObjects;

namespace Catalog.Domain.Models;

public class ColorVariant : Entity<ColorVariantId>
{
    public string Color { get; private set; } = default!;
    public List<string> Images { get; private set; } = new();
    public string Slug { get; private set; } = default!;
    public Price? Price { get; private set; }
    public List<SizeVariant>? Sizes { get; private set; } = new();

    public void UpdatePrice(decimal newPrice)
    {
        Price = new Price(newPrice);
    }

    // Autres méthodes business pour les variants
}