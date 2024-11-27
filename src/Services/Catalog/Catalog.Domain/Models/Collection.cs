namespace Catalog.Domain.Models;

public class Collection : Entity<CollectionId>
{
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!; 
    public Image CoverImage { get; private set; } = default!;

    private Collection()
    {
    }

    private Collection(CollectionId id, string name, string description, Image image)
    {
        Id = id;
        SetName(name);
        Description = description ?? string.Empty;
        CoverImage = image ?? throw new ArgumentNullException(nameof(image));
    }

    public static Collection Create(CollectionId collectionId, string name, string description, Image image)
    {
        return new Collection(collectionId, name, description, image);
    }

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
        Description = newDescription ?? string.Empty;
        LastModified = DateTime.UtcNow;
    }

    public void UpdateImage(Image newImage)
    {
        if (newImage == null)
        {
            throw new ArgumentNullException(nameof(newImage));
        }

        if (!CoverImage.Equals(newImage))
        {
            CoverImage = newImage;
            LastModified = DateTime.UtcNow;
        }
    }
}
