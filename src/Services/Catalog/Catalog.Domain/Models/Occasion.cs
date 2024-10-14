using Catalog.Domain.Abstractions;
using Catalog.Domain.ValueObjects;

namespace Catalog.Domain.Models;

public class Occasion : Entity<OccasionId>
{
    public string Name { get; private set; } = default!;

    public Occasion(OccasionId id, string name)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}