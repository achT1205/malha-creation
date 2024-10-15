namespace Catalog.Domain.Models;

public class Collection : Entity<CollectionId>
{
    public string Name { get; private set; } = default!;
    public string ImageSrc { get; private set; } = default!;
    public static Collection Create(string name, string imageSrc)
    {
        var occasion = new Collection
        {
            Id = CollectionId.Of(Guid.NewGuid()),
            Name = name ?? throw new ArgumentNullException(nameof(name)),
            ImageSrc = imageSrc ?? throw new ArgumentNullException(nameof(imageSrc))
        };

        return occasion;
    }
}

