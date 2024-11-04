namespace Catalog.Domain.ValueObjects;

public record ColorVariantQuantity
{
    public int? Value { get; private set; }

    private ColorVariantQuantity()
    {
        
    }
    private ColorVariantQuantity(int? value) => Value = value;
    public static ColorVariantQuantity Of(int? value)
    {

        return new ColorVariantQuantity(value);
    }

    public ColorVariantQuantity Increase(int? amount)
    {
        return new ColorVariantQuantity(Value + amount);
    }

    public ColorVariantQuantity Decrease(int amount)
    {
        return new ColorVariantQuantity(Value - amount);
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}