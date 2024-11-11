namespace Catalog.Domain.Models;
public class Brand : Entity<BrandId>
{
    public BrandName Name { get; private set; } = default!;

    private Brand()
    {

    }
    private Brand(BrandId id, BrandName name)
    {
        Id = id;
        Name = name;
    }

    public static Brand Create(BrandId brandId, BrandName name)
    {

        return new Brand(brandId, name);
    }
}
