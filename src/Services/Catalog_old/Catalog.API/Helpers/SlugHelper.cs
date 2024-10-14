using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;

namespace Catalog.API.Helpers;

public class SlugHelper
{
    public static string GenerateSlug(string productName, string color)
    {
        // Concatenate product name and color
        string slugInput = $"{productName} in {color}";

        // Convert to lowercase
        slugInput = slugInput.ToLowerInvariant();

        // Remove all invalid characters
        slugInput = RemoveDiacritics(slugInput); // Optional: Normalize and remove accents/diacritics
        slugInput = Regex.Replace(slugInput, @"[^a-z0-9\s-]", "");  // Keep only alphanumeric, spaces, and hyphens

        // Replace spaces with hyphens
        slugInput = Regex.Replace(slugInput, @"\s+", "-").Trim();

        // Optionally, truncate the slug to a reasonable length
        slugInput = slugInput.Substring(0, slugInput.Length <= 100 ? slugInput.Length : 100).Trim('-');

        return slugInput;
    }

    // Optional: Remove diacritics (accents on letters like é, à, etc.)
    private static string RemoveDiacritics(string text)
    {
        var normalizedString = text.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }
}
