using Catalog.Domain.Abstractions;
using Catalog.Domain.ValueObjects;

namespace Catalog.Domain.Models;

public class ProductType : Entity<ProductTypeId>
{
    public string Name { get; private set; } = default!;

    public ProductType(ProductTypeId id, string name)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}