namespace Catalog.Domain.ValueObjects;

public record Quantity
{
    public int Value { get; private set; }

    private Quantity()
    {
        
    }
    private Quantity(int value) => Value = value;
    public static Quantity Of(int value)
    {
        //ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value);

        return new Quantity(value);
    }

    public Quantity Increase(int amount)
    {
        return new Quantity(Value + amount);
    }

    public Quantity Decrease(int amount)
    {
        if (Value - amount < 0)
        {
            throw new InvalidOperationException("Quantity cannot be reduced below zero");
        }
        return new Quantity(Value - amount);
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}