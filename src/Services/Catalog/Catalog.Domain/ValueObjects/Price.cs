namespace Catalog.Domain.ValueObjects;

public record Price
{
    public const int Length = 3;

    public string Currency { get; private set; } = default!;
    public decimal Amount { get; private set; } = default!;

    private Price()
    {
        
    }
    private Price(string currency, decimal amount)
    {

        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency code cannot be null or empty.", nameof(currency));

        if (currency.Length != Length)
            throw new ArgumentException($"Currency code must be {Length} characters long.", nameof(currency));


        Currency = currency;

        if (amount < 0)
            throw new ArgumentException("Amount can't be negative.", nameof(amount));

        Amount = amount;
    }

    public static Price Of(string currency, decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException("Amount can't be negative.", nameof(amount));

        return new Price(currency, amount);
    }
}