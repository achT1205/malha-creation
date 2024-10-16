namespace Catalog.Domain.Models;


public class Category : Entity<CategoryId>
{
    public string Name { get; private set; }

    // Constructeur privé pour forcer l'utilisation de la méthode Create
    private Category(CategoryId id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Category Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Category name cannot be null or empty.", nameof(name));
        }

        if (name.Length > 100) // Exemple de contrainte sur la longueur
        {
            throw new ArgumentException("Category name cannot exceed 100 characters.", nameof(name));
        }

        return new Category(CategoryId.Of(Guid.NewGuid()), name);
    }
}
