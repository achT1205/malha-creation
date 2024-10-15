namespace Catalog.Domain.Models;

public class ProductType : Entity<ProductTypeId>
{
    public string Name { get; private set; } = default!;
    public static ProductType Create(string name)
    {
        var occasion = new ProductType
        {
            Id = ProductTypeId.Of(Guid.NewGuid()),
            Name = name ?? throw new ArgumentNullException(nameof(name))
        };

        return occasion;
    }
}