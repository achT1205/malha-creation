namespace Catalog.Domain.ValueObjects;

public record Price
{
    public decimal Value { get; }
    private Price(decimal value) => Value = value;
    public static Price Of(decimal value)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value);

        return new Price(value);
    }

    public Price Increase(decimal amount)
    {
        return new Price(Value + amount);
    }

    public Price Decrease(decimal amount)
    {
        if (Value - amount < 0)
        {
            throw new InvalidOperationException("Price cannot be reduced below zero");
        }
        return new Price(Value - amount);
    }
    public override string ToString()
    {
        return Value.ToString("C");  // Formats as currency
    }
}