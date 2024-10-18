namespace Catalog.Domain.ValueObjects;

public record Currency
{
    public const int Length = 3;

    public static readonly Currency USD = new("USD");
    public static readonly Currency EUR = new("EUR");

    public string Value { get; }

    private Currency(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Currency code cannot be null or empty.", nameof(value));

        if (value.Length != Length)
            throw new ArgumentException($"Currency code must be {Length} characters long.", nameof(value));

        Value = value;
    }

    public static Currency Of(string value)
    {
        return new Currency(value);
    }

    public override string ToString()
    {
        return Value;
    }
}
