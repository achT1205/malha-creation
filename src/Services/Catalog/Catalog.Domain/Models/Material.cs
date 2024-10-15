namespace Catalog.Domain.Models;

public class Material : Entity<MaterialId>
{
    public string Name { get; private set; } = default!;
    public static Material Create(string name)
    {
        var occasion = new Material
        {
            Id = MaterialId.Of(Guid.NewGuid()),
            Name = name ?? throw new ArgumentNullException(nameof(name))
        };

        return occasion;
    }
}