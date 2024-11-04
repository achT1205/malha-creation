namespace Catalog.Domain.Models;


public class Category : Entity<CategoryId>
{
    public CategoryName Name { get; private set; } = default!;

    private Category()
    {

    }
    private Category(CategoryId id, CategoryName name)
    {
        Id = id;
        Name = name;
    }

    public static Category Create( CategoryName name)
    {

        return new Category(CategoryId.Of(Guid.NewGuid()), name);
    }
}
