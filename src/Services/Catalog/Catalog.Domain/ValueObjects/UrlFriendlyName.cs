using System.Text.RegularExpressions;

namespace Catalog.Domain.ValueObjects;
public record UrlFriendlyName
{
    public string Value { get; private set; } = default!;

    private const int MaxLength = 100;

    private UrlFriendlyName()
    {
        
    }
    private UrlFriendlyName(string value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));

        if (value.Length > MaxLength)
        {
            throw new ArgumentException($"UrlFriendlyName cannot exceed {MaxLength} characters.");
        }

        // Vérification que la chaîne est compatible avec une URL
        if (!IsValidUrlFriendly(value))
        {
            throw new ArgumentException("UrlFriendlyName contains invalid characters.");
        }
    }

    public static UrlFriendlyName Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("UrlFriendlyName cannot be empty.", nameof(value));
        }

        return new UrlFriendlyName(value);
    }

    private static bool IsValidUrlFriendly(string value)
    {
        // Ajouter la logique de validation d'un nom d'URL (par exemple, pas de caractères spéciaux)
        return Regex.IsMatch(value, @"^[a-zA-Z0-9\-]+$");
    }
}

