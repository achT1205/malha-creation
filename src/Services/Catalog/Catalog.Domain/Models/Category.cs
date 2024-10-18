namespace Catalog.Domain.Models;


public class Category : Entity<CategoryId>
{
    public ProductId ProductId { get; private set; } = default!;
    public CategoryName Name { get; private set; } = default!;

    private Category()
    {
        
    }
    private Category(ProductId productId, CategoryId id, CategoryName name)
    {
        ProductId = productId;
        Id = id;
        Name = name;
    }

    public static Category Create(ProductId productId, CategoryId id, CategoryName name)
    {

        return new Category(productId, id, name);
    }
}
