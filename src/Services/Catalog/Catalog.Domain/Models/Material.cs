namespace Catalog.Domain.Models;

public class Material : Entity<MaterialId>
{
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    private Material(MaterialId id, string name, string description)
    {
        Id = id;
        SetName(name);
        Description = description ?? string.Empty;
    }

    private Material() { }

    public static Material Create(MaterialId materialId, string name, string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Description cannot be null or empty.", nameof(description));
        }

        return new Material(materialId, name, description);
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Material name cannot be null or empty.", nameof(name));
        }

        if (name.Length > 100)
        {
            throw new ArgumentException("Material name cannot exceed 100 characters.", nameof(name));
        }

        Name = name;
    }

    public void UpdateName(string newName)
    {
        if (!Name.Equals(newName, StringComparison.OrdinalIgnoreCase))
        {
            SetName(newName);
            LastModified = DateTime.UtcNow;
        }
    }

    public void UpdateDescription(string newDescription)
    {
        if (newDescription == null)
        {
            throw new ArgumentNullException(nameof(newDescription));
        }

        if (!Description.Equals(newDescription, StringComparison.OrdinalIgnoreCase))
        {
            Description = newDescription;
            LastModified = DateTime.UtcNow;
        }
    }
}
