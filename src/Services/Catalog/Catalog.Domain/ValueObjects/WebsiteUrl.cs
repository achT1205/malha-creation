namespace Catalog.Domain.ValueObjects;

public record WebsiteUrl
{
    public string Value { get; }

    private WebsiteUrl(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Website URL cannot be null or empty.", nameof(value));

        if (!Uri.TryCreate(value, UriKind.Absolute, out var uriResult) ||
            (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
            throw new ArgumentException("Website URL must be a valid HTTP or HTTPS URL.", nameof(value));

        Value = value;
    }

    public static WebsiteUrl Of(string value)
    {
        return new WebsiteUrl(value);
    }

    public override string ToString() => Value;
}
