namespace Catalog.Domain.ValueObjects;

public record Color
{
    public string Value { get; private set; } = default!;

    private Color()
    {
        
    }
    private Color(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Color cannot be null or empty.", nameof(value));
        }
        Value = value;
    }

    public static Color Of(string value) => new Color(value);

    public override string ToString() => Value;
}
