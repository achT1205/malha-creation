namespace Catalog.Domain.Models;

public class Occasion : Entity<OccasionId>
{
    public string Name { get; private set; } = default!;

    public static Occasion Create ( string name)
    {
        var occasion = new Occasion
        {
            Id = OccasionId.Of(Guid.NewGuid()),
            Name = name ?? throw new ArgumentNullException(nameof(name))
        };

        return occasion;
    }
}