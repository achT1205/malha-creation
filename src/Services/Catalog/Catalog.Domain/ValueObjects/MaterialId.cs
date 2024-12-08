public record MaterialId
{
    public Guid Value { get; private set; }
    private MaterialId()
    {
        
    }
    private MaterialId(Guid value) => Value = value;
    public static MaterialId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new CatalogDomainException("MaterialId cannot be empty.");
        }

        return new MaterialId(value);
    }
}