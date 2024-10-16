namespace Catalog.Domain.Models;
public class Image : Entity<ImageId>
{
    public string ImageSrc { get; private set; }
    public string AltText { get; private set; }

    private Image(ImageId id, string imageSrc, string altText)
    {
        if (string.IsNullOrWhiteSpace(imageSrc))
        {
            throw new ArgumentException("Image source cannot be null or empty.", nameof(imageSrc));
        }

        Id = id;
        ImageSrc = imageSrc;
        AltText = altText ?? string.Empty; // Optional alt text
    }

    public static Image Create(string imageSrc, string altText)
    {
        return new Image(ImageId.Of(Guid.NewGuid()), imageSrc, altText);
    }

    public void UpdateImageSrc(string newImageSrc)
    {
        if (string.IsNullOrWhiteSpace(newImageSrc))
        {
            throw new ArgumentNullException(nameof(newImageSrc), "Image source cannot be null or empty.");
        }

        if (!ImageSrc.Equals(newImageSrc))
        {
            ImageSrc = newImageSrc;
        }
    }

    public void UpdateAltText(string newAltText)
    {
        if (!AltText.Equals(newAltText))
        {
            AltText = newAltText ?? throw new ArgumentNullException(nameof(newAltText));
        }
    }
}