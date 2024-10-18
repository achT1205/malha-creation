namespace Catalog.Domain.ValueObjects;

public record ProductName
{
    public string Value { get; private set; } = default!;

    private const int MaxLength = 100;

    private ProductName()
    {
        
    }
    private ProductName(string value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));

        if (value.Length > MaxLength)
        {
            throw new ArgumentException($"Name cannot exceed {MaxLength} characters.");
        }
    }

    public static ProductName Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Name cannot be empty.", nameof(value));
        }

        return new ProductName(value);
    }
}
