namespace Catalog.Domain.Models;
public class Image : Entity<ImageId>
{
    public string ImageSrc { get; private set; } = default!;

    public static Image Create(string imageSrc)
    {
        var occasion = new Image
        {
            Id = ImageId.Of(Guid.NewGuid()),
            ImageSrc = imageSrc ?? throw new ArgumentNullException(nameof(imageSrc))
        };

        return occasion;
    }
}