namespace Catalog.Domain.Models;

public class Occasion : Entity<OccasionId>
{
    public ProductId ProductId { get; private set; } = default!;
    public OccasionName Name { get; private set; } = default!;

    private Occasion()
    {
        
    }
    private Occasion(ProductId productId, OccasionId id, OccasionName name)
    {
        Id = id;
        Name = name;
        ProductId = productId;
    }

    public static Occasion Create(ProductId productId, OccasionId id, OccasionName name)
    {
        return new Occasion(productId, id, name);
    }
}