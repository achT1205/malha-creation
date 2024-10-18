namespace Catalog.Domain.Models;

public class Collection : Entity<CollectionId>
{
    public string Name { get; private set; } = default!;
    public Image Image { get; private set; } = default!;

    private Collection()
    {
        
    }
    private Collection(CollectionId id, string name, Image image)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Image = image ?? throw new ArgumentNullException(nameof(image));
    }

    // Méthode statique de création pour encapsuler la logique de création
    public static Collection Create(string name, Image image)
    {
        return new Collection(CollectionId.Of(Guid.NewGuid()), name, image);
    }

    // Méthode privée pour valider et définir le nom
    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Collection name cannot be null or empty.", nameof(name));
        }

        if (name.Length > 100)
        {
            throw new ArgumentException("Collection name cannot exceed 100 characters.", nameof(name));
        }

        Name = name;
    }

    // Méthode pour mettre à jour le nom de la collection
    public void UpdateName(string newName)
    {
        if (!Name.Equals(newName, StringComparison.OrdinalIgnoreCase))
        {
            SetName(newName);
        }
    }

    // Méthode pour mettre à jour l'image associée à la collection
    public void UpdateImage(Image newImage)
    {
        if (newImage == null)
        {
            throw new ArgumentNullException(nameof(newImage));
        }

        if (!Image.Equals(newImage))
        {
            Image = newImage;
        }
    }
}
