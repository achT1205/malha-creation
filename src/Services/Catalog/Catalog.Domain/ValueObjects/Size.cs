namespace Catalog.Domain.ValueObjects;


public record Size
{
    private const int DefaultLength = 5;
    public string Value { get; private set; } = default!;

    private Size()
    {
        
    }

    private Size(string value)
    {
        Value = value;
    }

    public static Size Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Size cannot be null or empty.", nameof(value));
        }

        if (value.Length > DefaultLength)
        {
            throw new ArgumentOutOfRangeException(nameof(value), $"Size cannot be longer than {DefaultLength} characters.");
        }

        return new Size(value);
    }

    // Optional: Override ToString for better debugging experience
    public override string ToString() => Value;
}
