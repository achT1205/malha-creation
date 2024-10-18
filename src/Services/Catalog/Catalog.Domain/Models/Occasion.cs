namespace Catalog.Domain.Models;

public class Occasion : Entity<OccasionId>
{
    public OccasionName Name { get; private set; } = default!;

    private Occasion()
    {

    }
    private Occasion(OccasionId id, OccasionName name)
    {
        Id = id;
        Name = name;

    }

    public static Occasion Create( OccasionName name)
    {
        return new Occasion(OccasionId.Of(Guid.NewGuid()), name);
    }
}