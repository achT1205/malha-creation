namespace Catalog.Domain.Models;

public class ProductType : Entity<ProductTypeId>
{
    public string Name { get; private set; }

    private ProductType(ProductTypeId id, string name)
    {
        Id = id;
        Name = name;
    }

    public static ProductType Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Product type name cannot be null or empty.", nameof(name));
        }

        if (name.Length > 100) // Exemple de validation de longueur
        {
            throw new ArgumentException("Product type name cannot exceed 100 characters.", nameof(name));
        }

        return new ProductType(ProductTypeId.Of(Guid.NewGuid()), name);
    }
}
