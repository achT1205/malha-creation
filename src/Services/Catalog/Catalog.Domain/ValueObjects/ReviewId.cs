namespace Catalog.Domain.ValueObjects;

public record ReviewId
{
    public Guid Value { get; private set; }

    private ReviewId()
    {
        
    }
    private ReviewId(Guid value) => Value = value;
    public static ReviewId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("ReviewId cannot be empty.");
        }

        return new ReviewId(value);
    }
}