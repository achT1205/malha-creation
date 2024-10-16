namespace Catalog.Domain.Models;

public class Occasion : Entity<OccasionId>
{
    public string Name { get; private set; }

    // Constructeur privé pour forcer l'utilisation de la méthode Create
    private Occasion(OccasionId id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Occasion Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Occasion name cannot be null or empty.", nameof(name));
        }

        if (name.Length > 100) // Exemple de contrainte sur la longueur
        {
            throw new ArgumentException("Occasion name cannot exceed 100 characters.", nameof(name));
        }

        return new Occasion(OccasionId.Of(Guid.NewGuid()), name);
    }
}