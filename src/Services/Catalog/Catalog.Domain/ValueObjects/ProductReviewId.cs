namespace Catalog.Domain.ValueObjects;

public record ProductReviewId
{
    public Guid Value { get; private set; }
    private ProductReviewId()
    {
        
    }
    private ProductReviewId(Guid value) => Value = value;
    public static ProductReviewId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("ProductReviewId cannot be empty.");
        }

        return new ProductReviewId(value);
    }
}