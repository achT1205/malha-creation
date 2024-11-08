namespace Catalog.Infrastructure.Extentions;

internal class InitialData
{

    public static IEnumerable<Category> Categories => new List<Category>
     {
         Category.Create(CategoryName.Of("T-shirts")),
         Category.Create(CategoryName.Of("Pants")),
         Category.Create(CategoryName.Of("Jackets")),
         Category.Create(CategoryName.Of("Shoes")),
         Category.Create(CategoryName.Of("Bags")),
         Category.Create(CategoryName.Of("Accessories"))
     };

    public static IEnumerable<Collection> Collections => new List<Collection>
     {
         Collection.Create("Summer 2024",Image.Of( "Red T-shirt - Front", "/images/tshirt-red-front.jpg")),
         Collection.Create("Winter 2024", Image.Of( "Red T-shirt - Back", "/images/tshirt-red-back.jpg")),
         Collection.Create("Fall 2023", Image.Of( "Blue T-shirt - Front", "/images/tshirt-blue-front.jpg"))
     };

    public static IEnumerable<Occasion> Occasions =>
    new List<Occasion>
    {
         Occasion.Create(OccasionName.Of("Casual")),
         Occasion.Create(OccasionName.Of("Formal")),
         Occasion.Create(OccasionName.Of("Sportswear")),
         Occasion.Create(OccasionName.Of("Party")),
         Occasion.Create(OccasionName.Of("Workwear")),
         Occasion.Create(OccasionName.Of("Outdoor"))
    };

    public static IEnumerable<Material> Materials =>
    new List<Material>
    {
         Material.Create("Cotton"),
         Material.Create("Leather"),
         Material.Create("Denim"),
         Material.Create("Wool"),
         Material.Create("Silk"),
         Material.Create("Polyester")
    };

    public static IEnumerable<ProductType> ProductTypes =>
    new List<ProductType>
    {
         ProductType.Create("Clothing"),
         ProductType.Create("Footwear"),
         ProductType.Create("Accessories"),
         ProductType.Create("Bags"),
         ProductType.Create("Jewelry"),
         ProductType.Create("Outerwear")
    };

    public static IEnumerable<Brand> Brands =>
        new List<Brand>
        {
                 Brand.Create(BrandName.Of("ZARA")),
        };

}
