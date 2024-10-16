namespace Catalog.Infrastructure.Extentions;

internal class InitialData
{
   public static IEnumerable<Image> Images =>
   new List<Image>
   {
        Image.Create( "Red T-shirt - Front", "/images/tshirt-red-front.jpg"),
        Image.Create( "Red T-shirt - Back", "/images/tshirt-red-back.jpg"),
        Image.Create( "Blue T-shirt - Front", "/images/tshirt-blue-front.jpg"),
        Image.Create( "Blue T-shirt - Back", "/images/tshirt-blue-back.jpg"),
        Image.Create( "Bag - Front", "/images/bag-front.jpg"),
        Image.Create( "Bag - Bac", "/images/bag-back.jpg")
   };

    public static IEnumerable<Category> Categories => new List<Category>
    {
        Category.Create("T-shirts"),
        Category.Create("Pants"),
        Category.Create("Jackets"),
        Category.Create("Shoes"),
        Category.Create("Bags"),
        Category.Create("Accessories")
    };

    public static IEnumerable<Collection> Collections => new List<Collection>
    {
        Collection.Create("Summer 2024",Image.Create( "Red T-shirt - Front", "/images/tshirt-red-front.jpg")),
        Collection.Create("Winter 2024", Image.Create( "Red T-shirt - Back", "/images/tshirt-red-back.jpg")),
        Collection.Create("Fall 2023", Image.Create( "Blue T-shirt - Front", "/images/tshirt-blue-front.jpg"))
    };

    public static IEnumerable<Occasion> Occasions =>
    new List<Occasion>
    {
        Occasion.Create("Casual"),
        Occasion.Create("Formal"),
        Occasion.Create("Sportswear"),
        Occasion.Create("Party"),
        Occasion.Create("Workwear"),
        Occasion.Create("Outdoor")
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

    public static IEnumerable<SizeVariant> SizeVariants =>
    new List<SizeVariant>
    {
        SizeVariant.Create(Size.Of("S"), Price.Of(19.99m), Quantity.Of(10)),
        SizeVariant.Create(Size.Of("M"), Price.Of(21.99m), Quantity.Of(15)),
        SizeVariant.Create(Size.Of("L"), Price.Of(23.99m), Quantity.Of(5)),
        SizeVariant.Create(Size.Of("XL"), Price.Of(25.99m), Quantity.Of(8)),
        SizeVariant.Create(Size.Of("XXL"), Price.Of(27.99m), Quantity.Of(3))
    };

}
