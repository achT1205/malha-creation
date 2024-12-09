using System.Text.RegularExpressions;

namespace Catalog.Domain.ValueObjects;
public record UrlFriendlyName
{
    public string Value { get; private set; } = default!;

    private const int MaxLength = 300;

    private UrlFriendlyName()
    {
    }

    private UrlFriendlyName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("UrlFriendlyName cannot be empty.", nameof(value));
        }

        if (value.Length > MaxLength)
        {
            throw new ArgumentException($"UrlFriendlyName cannot exceed {MaxLength} characters.");
        }

        if (!IsValidUrlFriendly(value))
        {
            throw new ArgumentException("UrlFriendlyName contains invalid characters.");
        }

        value = NormalizeValue(value);

        Value = value;
    }

    public static UrlFriendlyName Of(string value)
    {
        return new UrlFriendlyName(value);
    }

    private static string NormalizeValue(string value)
    {
        // Réduction des espaces multiples à un seul
        value = Regex.Replace(value.Trim(), @"\s+", " ");

        // Remplacement des espaces par des tirets
        return value.Replace(" ", "-");
    }

    private static bool IsValidUrlFriendly(string value)
    {
        return Regex.IsMatch(value, @"^[a-zA-Z0-9\-]+$");
    }

    public override string ToString() => Value;
}
