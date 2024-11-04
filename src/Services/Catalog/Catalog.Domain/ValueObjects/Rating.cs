namespace Catalog.Domain.ValueObjects;

public record Rating
{
    public int Value { get; private set; } = default!;

    private Rating()
    {

    }
    private Rating(int value)
    {
        if (value < 1 || value > 5)
            throw new ArgumentException("Rating must be between 1 and 5.");
        Value = value;
    }

    public static Rating Of(int value) => new Rating(value);
}
