namespace Catalog.Domain.ValueObjects;

public record Size
{
    public string Value { get; private set; }

    public Size(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Size cannot be empty.");
        Value = value;
    }
}
