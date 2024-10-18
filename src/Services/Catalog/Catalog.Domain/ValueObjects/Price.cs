namespace Catalog.Domain.ValueObjects;

public record Price
{
    public Currency Currency { get; private set; } = default!;
    public decimal Amount { get; private set; } = default!;

    private Price()
    {
        
    }
    private Price(Currency currency, decimal amount)
    {
        Currency = currency;

        if (amount < 0)
            throw new ArgumentException("Amount can't be negative.", nameof(amount));

        Amount = amount;
    }

    public static Price Of(Currency currency, decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException("Amount can't be negative.", nameof(amount));

        return new Price(currency, amount);
    }
}