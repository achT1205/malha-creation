namespace Ordering.Domain.ValueObjects;
public record OrderCode
{
    private const int DefaultLength = 5;
    public string Value { get; }
    private OrderCode(string value) => Value = value;
    public static OrderCode Of(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        //ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, DefaultLength);

        return new OrderCode(value);
    }
}