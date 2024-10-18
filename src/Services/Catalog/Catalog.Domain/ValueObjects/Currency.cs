namespace Catalog.Domain.ValueObjects;

public sealed class Currency
{
    public const int Length = 100;

    public static readonly Currency USD = new("USD");
    public static readonly Currency EUR = new("EUR");

    private Currency()
    {
        
    }
    public string Value { get; private set; } = default!;

    private Currency(string value)
    {
        if (value.Length is not Length)
            throw new ArgumentException($"Length must be {Length}.", nameof(value));

        Value = value;
    }
}