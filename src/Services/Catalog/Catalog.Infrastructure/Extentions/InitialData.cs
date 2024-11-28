namespace Catalog.Infrastructure.Extentions;

internal class InitialData
{

    public static IEnumerable<Category> Categories => new List<Category>
{
    Category.Create(
        CategoryId.Of(new Guid("6cbe22ca-900f-4b38-9030-368e0f89bc73")),
        CategoryName.Of("T-shirts"),
        "A variety of stylish and comfortable t-shirts for all occasions.",
        Image.Of("https://placehold.co/200", "T-shirts Category Cover")
    ),
    Category.Create(
        CategoryId.Of(new Guid("6cbe22ca-900f-4b38-9030-368e0f89bc74")),
        CategoryName.Of("Pants"),
        "Discover a range of pants including jeans, chinos, and casual wear.",
        Image.Of("https://placehold.co/200", "Pants Category Cover")
    ),
    Category.Create(
        CategoryId.Of(new Guid("6cbe22ca-900f-4b38-9030-368e0f89bc75")),
        CategoryName.Of("Jackets"),
        "High-quality jackets for every season and style preference.",
        Image.Of("https://placehold.co/200", "Jackets Category Cover")
    ),
    Category.Create(
        CategoryId.Of(new Guid("6cbe22ca-900f-4b38-9030-368e0f89bc76")),
        CategoryName.Of("Shoes"),
        "Shoes for every occasion, including casual, formal, and sportswear.",
        Image.Of("https://placehold.co/200", "Shoes Category Cover")
    ),
    Category.Create(
        CategoryId.Of(new Guid("6cbe22ca-900f-4b38-9030-368e0f89bc77")),
        CategoryName.Of("Bags"),
        "Explore our collection of stylish and functional bags.",
        Image.Of("https://placehold.co/200", "Bags Category Cover")
    )
};


    public static IEnumerable<Collection> Collections => new List<Collection>
{
    Collection.Create(
        CollectionId.Of(new Guid("e6ef95a7-29f7-4ec4-8984-0d8602c94b26")),
        "Summer 2024",
        "A vibrant collection featuring lightweight and trendy apparel for the summer season.",
        Image.Of("https://placehold.co/300", "Summer 2024 Collection Cover")
    ),
    Collection.Create(
        CollectionId.Of(new Guid("e6ef95a7-29f7-4ec4-8984-0d8602c94b27")),
        "Winter 2024",
        "Stay warm with our Winter 2024 collection, including cozy jackets, scarves, and boots.",
        Image.Of("https://placehold.co/300", "Winter 2024 Collection Cover")
    ),
    Collection.Create(
        CollectionId.Of(new Guid("e6ef95a7-29f7-4ec4-8984-0d8602c94b28")),
        "Fall 2023",
        "Embrace autumn with our Fall 2023 collection, featuring earthy tones and layered outfits.",
        Image.Of("https://placehold.co/300", "Fall 2023 Collection Cover")
    )
};


    public static IEnumerable<Occasion> Occasions =>
        new List<Occasion>
        {
        Occasion.Create(
            OccasionId.Of(new Guid("b3c6c410-d05a-4426-a6a6-2f086901d411")),
            OccasionName.Of("Casual"),
            "Relaxed and comfortable clothing suitable for everyday activities."
        ),
        Occasion.Create(
            OccasionId.Of(new Guid("b3c6c410-d05a-4426-a6a6-2f086901d412")),
            OccasionName.Of("Formal"),
            "Elegant and polished outfits perfect for formal events and occasions."
        ),
        Occasion.Create(
            OccasionId.Of(new Guid("b3c6c410-d05a-4426-a6a6-2f086901d413")),
            OccasionName.Of("Sportswear"),
            "Clothing designed for physical activities and sports, combining comfort and performance."
        ),
        Occasion.Create(
            OccasionId.Of(new Guid("b3c6c410-d05a-4426-a6a6-2f086901d414")),
            OccasionName.Of("Party"),
            "Trendy and stylish outfits ideal for parties and social gatherings."
        ),
        Occasion.Create(
            OccasionId.Of(new Guid("b3c6c410-d05a-4426-a6a6-2f086901d415")),
            OccasionName.Of("Workwear"),
            "Professional attire suited for office settings and corporate environments."
        ),
        Occasion.Create(
            OccasionId.Of(new Guid("b3c6c410-d05a-4426-a6a6-2f086901d416")),
            OccasionName.Of("Outdoor"),
            "Durable and weather-appropriate clothing designed for outdoor adventures and activities."
        )
        };


    public static IEnumerable<Material> Materials =>
        new List<Material>
        {
        Material.Create(
            MaterialId.Of(new Guid("a3addcb2-ce9c-4ca3-9068-6a8b8eccf711")),
            "Cotton",
            "A soft, breathable, and durable natural fiber widely used in clothing."
        ),
        Material.Create(
            MaterialId.Of(new Guid("a3addcb2-ce9c-4ca3-9068-6a8b8eccf712")),
            "Leather",
            "A strong and flexible material made from animal hide, often used in shoes, bags, and jackets."
        ),
        Material.Create(
            MaterialId.Of(new Guid("a3addcb2-ce9c-4ca3-9068-6a8b8eccf713")),
            "Denim",
            "A sturdy cotton fabric, typically blue, used for jeans and casual wear."
        ),
        Material.Create(
            MaterialId.Of(new Guid("a3addcb2-ce9c-4ca3-9068-6a8b8eccf714")),
            "Wool",
            "A natural fiber obtained from sheep, known for its warmth and softness."
        ),
        Material.Create(
            MaterialId.Of(new Guid("a3addcb2-ce9c-4ca3-9068-6a8b8eccf715")),
            "Silk",
            "A luxurious natural fiber known for its smooth texture and sheen, often used in high-end fashion."
        ),
        Material.Create(
            MaterialId.Of(new Guid("a3addcb2-ce9c-4ca3-9068-6a8b8eccf716")),
            "Polyester",
            "A synthetic fiber known for its durability, resistance to wrinkles, and affordability."
        )
        };

    //public static IEnumerable<ProductType> ProductTypes =>
    //new List<ProductType>
    //{
    //     ProductType.Create("Clothing"),
    //     ProductType.Create("Footwear"),
    //     ProductType.Create("Accessories"),
    //     ProductType.Create("Bags"),
    //     ProductType.Create("Jewelry"),
    //     ProductType.Create("Outerwear")
    //};

    public static IEnumerable<Brand> Brands =>
        new List<Brand>
        {
        Brand.Create(
            BrandId.Of(new Guid("b7bcda18-05cc-450b-b99a-3c5eafe111a4")),
            BrandName.Of("ZARA"),
            "ZARA is a leading international fashion retailer offering trendy apparel and accessories.",
            WebsiteUrl.Of("https://www.zara.com"),
            Image.Of("https://placehold.co/300", "ZARA Logo")
        ),
        Brand.Create(
            BrandId.Of(new Guid("b7bcda18-05cc-450b-b99a-3c5eafe111a5")),
            BrandName.Of("C&A"),
            "C&A provides affordable fashion for men, women, and children worldwide.",
            WebsiteUrl.Of("https://www.c-and-a.com"),
            Image.Of("https://placehold.co/300", "C&A Logo")
        ),
        Brand.Create(
            BrandId.Of(new Guid("b7bcda18-05cc-450b-b99a-3c5eafe111a6")),
            BrandName.Of("H&M"),
            "H&M offers fashion and quality at the best price in a sustainable way.",
            WebsiteUrl.Of("https://www.hm.com"),
            Image.Of("https://placehold.co/300", "H&M Logo")
        ),
        Brand.Create(
            BrandId.Of(new Guid("b7bcda18-05cc-450b-b99a-3c5eafe111a7")),
            BrandName.Of("Nike"),
            "Nike is a world-renowned brand known for its athletic apparel, footwear, and accessories.",
            WebsiteUrl.Of("https://www.nike.com"),
            Image.Of("https://placehold.co/300", "Nike Logo")
        ),
        Brand.Create(
            BrandId.Of(new Guid("b7bcda18-05cc-450b-b99a-3c5eafe111a8")),
            BrandName.Of("Adidas"),
            "Adidas is a global leader in sportswear manufacturing, producing high-quality products.",
            WebsiteUrl.Of("https://www.adidas.com"),
            Image.Of("https://placehold.co/300", "Adidas Logo")
        )
        };


    public static IEnumerable<Product> Products =>
            new List<Product>
            {
            NewClothingProduct(
                name: "T-shirt Sport",
                friendlyName: "t-shirt-sport",
                description: "T-shirt respirant pour le sport",
                colors: new List<string> { "Black", "Grey" },
                sizes: new List<string> { "M", "L", "XL" }
            ),
            NewClothingProduct(
                name: "T-shirt Basic",
                friendlyName: "t-shirt-basic",
                description: "T-shirt confortable en coton",
                colors: new List<string> { "Red", "Blue" },
                sizes: new List<string> { "S", "M", "L" }
            ),
            NewClothingProduct(
                name: "Pantalon Cargo",
                friendlyName: "pantalon-cargo",
                description: "Pantalon avec poches multiples, style décontracté",
                colors: new List<string> { "Kaki", "Beige" },
                sizes: new List<string> { "32", "34", "36" }
            ),
            NewClothingProduct(
                name: "Pantalon Classique",
                friendlyName: "pantalon-classique",
                description: "Pantalon élégant pour des occasions formelles",
                colors: new List<string> { "Black", "Blue" },
                sizes: new List<string> { "30", "32", "34" }
            ),
            NewAccessoryProduct(
                name: "Sac à Dos Urban",
                friendlyName: "sac-dos-urban",
                description: "Sac à dos moderne pour usage quotidien",
                colors: new List<string> { "Black", "Grey" }
            ),
            NewAccessoryProduct(
                name: "Sac à Main Élégant",
                friendlyName: "sac-main-elegant",
                description: "Sac à main élégant pour des occasions formelles",
                colors: new List<string> { "Red", "Blue" }
            )
            };

    private static Product NewClothingProduct(string name, string friendlyName, string description, List<string> colors, List<string> sizes)
    {
        var urlFriendlyName = UrlFriendlyName.Of(friendlyName);
        var product = Product.Create(
                ProductName.Of(friendlyName),
                urlFriendlyName,
                ProductDescription.Of(description),
                true,
                Image.Of($"src-{friendlyName}", $"alt-{friendlyName}"),
                Domain.Enums.ProductType.Clothing,
                Materials.First(_ => _.Name == "Cotton").Id,
                Brands.First(_ => _.Name.Value == "ZARA").Id,
                Collections.First(_ => _.Name == "Winter 2024").Id);


        foreach (var c in colors)
        {
            var color = Color.Of(c);
            var cv = ColorVariant.Create(
               product.Id,
               color,
               Slug.Of(urlFriendlyName, color),
               ColorVariantPrice.Of(null, null),
               ColorVariantQuantity.Of(null),
               ColorVariantQuantity.Of(null)
               );

            foreach (var s in sizes)
            {
                var sv = SizeVariant.Create(
                cv.Id,
                Size.Of(s),
                Price.Of("USD", 15),
                Quantity.Of(80),
                Quantity.Of(10));
                cv.AddSizeVariant(sv);
            }
            product.AddColorVariant(cv);
        }
        return product;
    }


    private static Product NewAccessoryProduct(string name, string friendlyName, string description, List<string> colors)
    {
        var urlFriendlyName = UrlFriendlyName.Of(friendlyName);
        var product = Product.Create(
                ProductName.Of(friendlyName),
                urlFriendlyName,
                ProductDescription.Of(description),
                true,
                Image.Of($"src-{friendlyName}", $"alt-{friendlyName}"),
                Domain.Enums.ProductType.Accessory,
                Materials.First(_ => _.Name == "Leather").Id,
                Brands.First(_ => _.Name.Value == "ZARA").Id,
                Collections.First(_ => _.Name == "Winter 2024").Id);

        foreach (var c in colors)
        {
            var color = Color.Of(c);
            var cv = ColorVariant.Create(
               product.Id,
               color,
               Slug.Of(urlFriendlyName, color),
               ColorVariantPrice.Of("USD", 15),
               ColorVariantQuantity.Of(80),
               ColorVariantQuantity.Of(10)
               );
            product.AddColorVariant(cv);
        }
        return product;
    }

}
