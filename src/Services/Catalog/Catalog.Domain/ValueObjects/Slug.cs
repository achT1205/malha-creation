namespace Catalog.Domain.ValueObjects;

public record Slug
{
    public string Value { get; private set; } = default!;

    private Slug()
    {
        
    }
    private Slug(string value)
    {
        Value = value;
    }

    // Méthode pour créer le slug avec validation
    public static Slug Of(string urlFriendlyName, string color)
    {
        if (string.IsNullOrWhiteSpace(urlFriendlyName))
        {
            throw new ArgumentException("UrlFriendlyName cannot be null or empty.", nameof(urlFriendlyName));
        }

        if (string.IsNullOrWhiteSpace(color))
        {
            throw new ArgumentException("Color cannot be null or empty.", nameof(color));
        }

        // Génération du slug
        string slug = GenerateSlug(urlFriendlyName, color);

        return new Slug(slug);
    }

    private static string GenerateSlug(string urlFriendlyName, string color)
    {
        // Concatenate product name and color
        string slugInput = $"{urlFriendlyName} in {color}";

        // Convert to lowercase
        slugInput = slugInput.ToLowerInvariant();

        // Remove diacritics
        slugInput = RemoveDiacritics(slugInput);

        // Remove all invalid characters (keep only alphanumeric, spaces, and hyphens)
        slugInput = System.Text.RegularExpressions.Regex.Replace(slugInput, @"[^a-z0-9\s-]", "");

        // Replace spaces with hyphens
        slugInput = System.Text.RegularExpressions.Regex.Replace(slugInput, @"\s+", "-").Trim();

        // Truncate to 100 characters if needed
        slugInput = slugInput.Substring(0, slugInput.Length <= 100 ? slugInput.Length : 100).Trim('-');

        return slugInput;
    }

    // Optional: Remove diacritics (accents on letters like é, à, etc.)
    private static string RemoveDiacritics(string text)
    {
        var normalizedString = text.Normalize(System.Text.NormalizationForm.FormD);
        var stringBuilder = new System.Text.StringBuilder();

        foreach (var c in normalizedString)
        {
            var unicodeCategory = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != System.Globalization.UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString().Normalize(System.Text.NormalizationForm.FormC);
    }

    public override string ToString() => Value;
}

