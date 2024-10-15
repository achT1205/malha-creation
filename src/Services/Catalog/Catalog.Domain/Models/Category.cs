namespace Catalog.Domain.Models;


public class Category : Entity<CategoryId>
{
    public string Name { get; private set; } = default!;

    public static Category Create(string name)
    {
        var occasion = new Category
        {
            Id = CategoryId.Of(Guid.NewGuid()),
            Name = name ?? throw new ArgumentNullException(nameof(name))
        };

        return occasion;
    }
}