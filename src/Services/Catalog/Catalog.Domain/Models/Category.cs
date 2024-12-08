namespace Catalog.Domain.Models;
public class Category : Entity<CategoryId>
{
    public CategoryName Name { get; private set; } = default!;
    public string Description { get; private set; } = default!; 
    public Image CoverImage { get; private set; } = default!;
    private Category()
    {
    }

    private Category(CategoryId id, CategoryName name, string description, Image coverImage)
    {
        Id = id;
        Name = name;
        Description = description;
        CoverImage = coverImage;
    }

    public static Category Create(CategoryId categoryId, CategoryName name, string description, Image coverImage)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description is required.", nameof(description));

        if (coverImage == null)
            throw new ArgumentNullException(nameof(coverImage), "Cover image is required.");

        return new Category(categoryId, name, description, coverImage);
    }

    public void UpdateDetails(CategoryName newName, string newDescription, Image newCoverImage)
    {
        if (newName == null)
            throw new ArgumentNullException(nameof(newName), "Category name cannot be null.");

        if (string.IsNullOrWhiteSpace(newDescription))
            throw new ArgumentException("Description cannot be empty.", nameof(newDescription));

        if (newCoverImage == null)
            throw new ArgumentNullException(nameof(newCoverImage), "Cover image cannot be null.");

        Name = newName;
        Description = newDescription;
        CoverImage = newCoverImage;
        LastModified = DateTime.UtcNow;
    }
}
