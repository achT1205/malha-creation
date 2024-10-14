namespace Catalog.Domain.ValueObjects;

public record OccasionId
{
    public Guid Value { get; }
    private OccasionId(Guid value) => Value = value;
    public static OccasionId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("OccasionId cannot be empty.");
        }

        return new OccasionId(value);
    }
}