namespace Catalog.Domain.Models;

public class Material : Entity<MaterialId>
{
    public string Name { get; private set; }

    // Constructeur privé pour garantir que Material est créé via la méthode Create
    private Material(MaterialId id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Material Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Material name cannot be null or empty.", nameof(name));
        }

        if (name.Length > 100) // Exemple de contrainte sur la longueur
        {
            throw new ArgumentException("Material name cannot exceed 100 characters.", nameof(name));
        }

        return new Material(MaterialId.Of(Guid.NewGuid()), name);
    }
}