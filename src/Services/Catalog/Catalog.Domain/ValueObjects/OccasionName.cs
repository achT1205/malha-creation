namespace Catalog.Domain.ValueObjects;

public record OccasionName
{
    public string Value { get; private set; } = default!;

    private const int MaxLength = 100;

    private OccasionName()
    {
        
    }
    private OccasionName(string value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));

        if (value.Length > MaxLength)
        {
            throw new ArgumentException($"Name cannot exceed {MaxLength} characters.");
        }
    }

    public static OccasionName Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Name cannot be empty.", nameof(value));
        }

        return new OccasionName(value);
    }
}
