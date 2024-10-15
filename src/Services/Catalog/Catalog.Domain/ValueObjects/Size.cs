namespace Catalog.Domain.ValueObjects;


public record Size
{
    private const int DefaultLength = 5;
    public string Value { get; }
    private Size(string value) => Value = value;
    public static Size Of(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(value.Length, DefaultLength);

        return new Size(value);
    }
}