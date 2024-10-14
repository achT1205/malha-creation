using Catalog.Domain.Abstractions;
using Catalog.Domain.ValueObjects;

namespace Catalog.Domain.Models;

public class Collection : Entity<CollectionId>
{
    public string Name { get; private set; } = default!;
    public Collection(CollectionId id, string name, DateTime createdAt)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        CreatedAt = createdAt;
    }
}