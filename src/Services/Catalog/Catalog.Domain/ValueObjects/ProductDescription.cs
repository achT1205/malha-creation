namespace Catalog.Domain.ValueObjects;

public record ProductDescription
{
    public string Value { get; private set; } = default!;

    private const int MaxLength = 1000;

    private ProductDescription()
    {
        
    }
    private ProductDescription(string value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));

        if (value.Length > MaxLength)
        {
            throw new ArgumentException($"Description cannot exceed {MaxLength} characters.");
        }
    }

    public static ProductDescription Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Description cannot be empty.", nameof(value));
        }

        return new ProductDescription(value);
    }
}
