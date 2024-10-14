using Catalog.Domain.Abstractions;

namespace Catalog.Domain.Models;

public class Material : Entity<MaterialId>
{
    public string Name { get; private set; } = default!;

    public Material(MaterialId id, string name)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}