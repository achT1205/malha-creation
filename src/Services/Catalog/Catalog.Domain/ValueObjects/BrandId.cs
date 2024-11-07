
namespace Catalog.Domain.ValueObjects;

public record BrandId
{
    public Guid Value { get; private set; }
    private BrandId()
    {
        
    }
    private BrandId(Guid value) => Value = value;
    public static BrandId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new CatalogDomainException("BrandId cannot be empty.");
        }

        return new BrandId(value);
    }
}