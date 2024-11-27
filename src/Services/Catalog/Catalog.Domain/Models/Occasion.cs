public class Occasion : Entity<OccasionId>
{
    public OccasionName Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    private Occasion()
    {
    }

    private Occasion(OccasionId id, OccasionName name, string description)
    {
        Id = id;
        SetName(name);
        Description = description ?? string.Empty;
    }

    public static Occasion Create(OccasionId occasionId, OccasionName name, string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Description cannot be null or empty.", nameof(description));
        }

        return new Occasion(occasionId, name, description);
    }

    private void SetName(OccasionName name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name), "Occasion name cannot be null.");
    }

    public void UpdateName(OccasionName newName)
    {
        if (!Name.Equals(newName))
        {
            SetName(newName);
            LastModified = DateTime.UtcNow;
        }
    }

    public void UpdateDescription(string newDescription)
    {
        if (newDescription == null)
        {
            throw new ArgumentNullException(nameof(newDescription));
        }

        if (!Description.Equals(newDescription, StringComparison.OrdinalIgnoreCase))
        {
            Description = newDescription;
            LastModified = DateTime.UtcNow;
        }
    }
}
