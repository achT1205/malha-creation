namespace Discount.Domain.ValueObjects;

public record CouponCode
{
    public string Value { get; private set; } = string.Empty;
    private CouponCode(){}

    private CouponCode(string value)
    {
        Value = value;
    }

    public static CouponCode Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Coupon code cannot be null or empty.", nameof(value));

        if (!IsValidFormat(value))
            throw new ArgumentException("Invalid coupon code format.", nameof(value));

        return new CouponCode(value);
    }

    private static bool IsValidFormat(string code)
    {
        // Example: Ensure code is alphanumeric and between 5-15 characters
        return code.Length >= 5 && code.Length <= 15 && code.All(char.IsLetterOrDigit);
    }
}
