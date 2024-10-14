namespace Catalog.Domain.ValueObjects;

public record Price
{
    public decimal Value { get; private set; }

    private Price(decimal value)
    {
        if (value < 0) throw new ArgumentException("Price cannot be negative.");
        Value = value;
    }
}
