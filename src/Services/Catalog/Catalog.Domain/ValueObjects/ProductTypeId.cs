namespace Catalog.Domain.ValueObjects;

public record ProductTypeId
{
    public Guid Value { get; private set; }
    private ProductTypeId()
    {
        
    }
    private ProductTypeId(Guid value) => Value = value;
    public static ProductTypeId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new CatalogDomainException("ProductTypeId cannot be empty.");
        }

        return new ProductTypeId(value);
    }
}