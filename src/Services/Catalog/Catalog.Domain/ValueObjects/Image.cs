namespace Catalog.Domain.ValueObjects;

public record Image
{
    public string ImageSrc { get; private set; } = default!;
    public string AltText { get; private set; } = default!;

    private const int MaxImageSrcLength = 2048; // Exemple de contrainte de longueur max

    private Image()
    {
        
    }
    private Image(string imageSrc, string altText)
    {
        if (string.IsNullOrWhiteSpace(imageSrc))
        {
            throw new ArgumentException("Image source cannot be null or empty.", nameof(imageSrc));
        }

        if (imageSrc.Length > MaxImageSrcLength)
        {
            throw new ArgumentException($"Image source cannot exceed {MaxImageSrcLength} characters.", nameof(imageSrc));
        }

        ImageSrc = imageSrc;
        AltText = altText ?? string.Empty; // AltText peut être vide
    }

    public static Image Of(string imageSrc, string altText)
    {
        return new Image(imageSrc, altText);
    }
}
