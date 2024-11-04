namespace Catalog.Domain.ValueObjects;

public record CategoryName
{
    public string Value { get; private set; } = default!;

    private const int MaxLength = 100;

    private CategoryName()
    {
        
    }
    private CategoryName(string value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));

        if (value.Length > MaxLength)
        {
            throw new ArgumentException($"Name cannot exceed {MaxLength} characters.");
        }
    }

    public static CategoryName Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Name cannot be empty.", nameof(value));
        }

        return new CategoryName(value);
    }
}
