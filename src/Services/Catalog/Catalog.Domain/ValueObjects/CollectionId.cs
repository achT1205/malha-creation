namespace Catalog.Domain.ValueObjects;

public record CollectionId
{
    public Guid Value { get; private set; }
    private CollectionId()
    {
        
    }
    private CollectionId(Guid value) => Value = value;
    public static CollectionId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new CatalogDomainException("CollectionId cannot be empty.");
        }

        return new CollectionId(value);
    }
}