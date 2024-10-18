namespace Catalog.Domain.ValueObjects;

public record AverageRating
{
    public decimal Value { get; private set; }
    public int TotalRatingsCount { get; private set; }

    private const decimal MinRating = 0m;
    private const decimal MaxRating = 5m;
    private const int MaxDecimalPlaces = 2;

    private AverageRating()
    {
        
    }
    private AverageRating(decimal value, int totalRatingsCount)
    {
        if (value < MinRating || value > MaxRating)
        {
            throw new ArgumentException($"Rating must be between {MinRating} and {MaxRating}.", nameof(value));
        }

        if (totalRatingsCount < 0)
        {
            throw new ArgumentException("Total ratings count cannot be negative.", nameof(totalRatingsCount));
        }

        // Arrondir à deux décimales
        Value = Math.Round(value, MaxDecimalPlaces);
        TotalRatingsCount = totalRatingsCount;
    }

    public static AverageRating Of(decimal value, int totalRatingsCount)
    {
        return new AverageRating(value, totalRatingsCount);
    }

    // Recalculer la moyenne avec un nouveau rating
    public AverageRating AddNewRating(decimal newRating)
    {
        if (newRating < MinRating || newRating > MaxRating)
        {
            throw new ArgumentException($"Rating must be between {MinRating} and {MaxRating}.", nameof(newRating));
        }

        var newTotalRatings = TotalRatingsCount + 1;
        var newAverage = ((Value * TotalRatingsCount) + newRating) / newTotalRatings;
        return Of(newAverage, newTotalRatings);
    }
}

