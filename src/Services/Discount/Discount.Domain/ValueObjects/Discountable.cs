namespace Discount.Domain.ValueObjects;

public record Discountable
{
    public decimal? FlatAmount { get; private set; }
    public int? Percentage { get; private set; }

    private Discountable() { }

    private Discountable(decimal? flatAmount, int? percentage)
    {
        if (flatAmount.HasValue && flatAmount < 0)
            throw new ArgumentException("Flat amount cannot be negative.", nameof(flatAmount));

        if (percentage.HasValue && (percentage < 0 || percentage > 100))
            throw new ArgumentException("Percentage must be between 1 and 100.", nameof(percentage));

        if ((!flatAmount.HasValue && !percentage.HasValue)||(flatAmount == 0 && percentage == 0))
            throw new ArgumentException("Either flat amount or percentage must be provided.");

        if ((flatAmount.HasValue && flatAmount > 0) && (percentage.HasValue && percentage > 0 && percentage < 100))
            throw new ArgumentException("Either flat amount or percentage must be provided.");

        FlatAmount = flatAmount;
        Percentage = percentage;
    }

    public static Discountable Of(decimal? flatAmount, int? percentage)
    {
        return new Discountable(flatAmount, percentage);
    }

    public decimal CalculateDiscount(decimal total)
    {
        if (FlatAmount.HasValue) return Math.Max(0, total - FlatAmount.Value);
        if (Percentage.HasValue) return Math.Max(0, total * (1 - (Percentage.Value / 100m)));
        return total;
    }
}