namespace Catalog.Domain.Models;
public class Brand : Entity<BrandId>
{
    public BrandName Name { get; private set; } = default!;
    public string Description { get; private set; } = default!; // Short description of the brand
    public WebsiteUrl WebsiteUrl { get; private set; } = default!; // Official website link
    public Image Logo { get; private set; } = default!; // Brand logo as an image object

    private Brand()
    {
    }

    private Brand(BrandId id, BrandName name, string description, WebsiteUrl websiteUrl, Image logo)
    {
        Id = id;
        Name = name;
        Description = description;
        WebsiteUrl = websiteUrl;
        Logo = logo;
    }

    public static Brand Create(BrandId brandId, BrandName name, string description, WebsiteUrl websiteUrl, Image logo)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description is required.", nameof(description));

        if (logo == null)
            throw new ArgumentNullException(nameof(logo), "Logo is required.");

        return new Brand(brandId, name, description, websiteUrl, logo);
    }

    public void UpdateDetails(string newDescription, WebsiteUrl newWebsiteUrl, Image newLogo)
    {
        if (string.IsNullOrWhiteSpace(newDescription))
            throw new ArgumentException("Description cannot be empty.", nameof(newDescription));

        if (newLogo == null)
            throw new ArgumentNullException(nameof(newLogo), "Logo cannot be null.");

        Description = newDescription;
        WebsiteUrl = newWebsiteUrl;
        Logo = newLogo;
        LastModified = DateTime.UtcNow;
    }
}
