namespace Catalog.Domain.ValueObjects;

public record ColorVariantPrice
{
    public const int Length = 3;

    public string? Currency { get; private set; } = default!;
    public decimal? Amount { get; private set; } = default!;

    private ColorVariantPrice()
    {
        
    }
    private ColorVariantPrice(string? currency, decimal? amount)
    {
        Currency = currency;
        Amount = amount;
    }

    public static ColorVariantPrice Of(string? currency, decimal? amount)
    {
        return new ColorVariantPrice(currency, amount);
    }
}