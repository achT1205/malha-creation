namespace Catalog.Infrastructure.Extentions;

internal class InitialData
{

    public static IEnumerable<Category> Categories => new List<Category>
     {
         Category.Create(CategoryId.Of(new Guid("6cbe22ca-900f-4b38-9030-368e0f89bc73")), CategoryName.Of("T-shirts")),
         Category.Create(CategoryId.Of(new Guid("6cbe22ca-900f-4b38-9030-368e0f89bc74")), CategoryName.Of("Pants")),
         Category.Create(CategoryId.Of(new Guid("6cbe22ca-900f-4b38-9030-368e0f89bc75")), CategoryName.Of("Jackets")),
         Category.Create(CategoryId.Of(new Guid("6cbe22ca-900f-4b38-9030-368e0f89bc76")), CategoryName.Of("Shoes")),
         Category.Create(CategoryId.Of(new Guid("6cbe22ca-900f-4b38-9030-368e0f89bc77")), CategoryName.Of("Bags"))
     };

    public static IEnumerable<Collection> Collections => new List<Collection>
     {
         Collection.Create(CollectionId.Of(new Guid("e6ef95a7-29f7-4ec4-8984-0d8602c94b26")),"Summer 2024",Image.Of( "Red T-shirt - Front", "/images/tshirt-red-front.jpg")),
         Collection.Create(CollectionId.Of(new Guid("e6ef95a7-29f7-4ec4-8984-0d8602c94b27")), "Winter 2024", Image.Of( "Red T-shirt - Back", "/images/tshirt-red-back.jpg")),
         Collection.Create(CollectionId.Of(new Guid("e6ef95a7-29f7-4ec4-8984-0d8602c94b28")), "Fall 2023", Image.Of( "Blue T-shirt - Front", "/images/tshirt-blue-front.jpg"))
     };

    public static IEnumerable<Occasion> Occasions =>
    new List<Occasion>
    {
         Occasion.Create(OccasionId.Of(new Guid("b3c6c410-d05a-4426-a6a6-2f086901d411")), OccasionName.Of("Casual")),
         Occasion.Create(OccasionId.Of(new Guid("b3c6c410-d05a-4426-a6a6-2f086901d412")), OccasionName.Of("Formal")),
         Occasion.Create(OccasionId.Of(new Guid("b3c6c410-d05a-4426-a6a6-2f086901d413")), OccasionName.Of("Sportswear")),
         Occasion.Create(OccasionId.Of(new Guid("b3c6c410-d05a-4426-a6a6-2f086901d414")), OccasionName.Of("Party")),
         Occasion.Create(OccasionId.Of(new Guid("b3c6c410-d05a-4426-a6a6-2f086901d415")), OccasionName.Of("Workwear")),
         Occasion.Create(OccasionId.Of(new Guid("b3c6c410-d05a-4426-a6a6-2f086901d416")), OccasionName.Of("Outdoor"))
    };

    public static IEnumerable<Material> Materials =>
    new List<Material>
    {
         Material.Create(MaterialId.Of(new Guid("a3addcb2-ce9c-4ca3-9068-6a8b8eccf711")),"Cotton"),
         Material.Create(MaterialId.Of(new Guid("a3addcb2-ce9c-4ca3-9068-6a8b8eccf712")), "Leather"),
         Material.Create(MaterialId.Of(new Guid("a3addcb2-ce9c-4ca3-9068-6a8b8eccf713")), "Denim"),
         Material.Create(MaterialId.Of(new Guid("a3addcb2-ce9c-4ca3-9068-6a8b8eccf714")), "Wool"),
         Material.Create(MaterialId.Of(new Guid("a3addcb2-ce9c-4ca3-9068-6a8b8eccf715")), "Silk"),
         Material.Create(MaterialId.Of(new Guid("a3addcb2-ce9c-4ca3-9068-6a8b8eccf716")), "Polyester")
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
                 Brand.Create(BrandId.Of(new Guid("b7bcda18-05cc-450b-b99a-3c5eafe111a4")), BrandName.Of("ZARA")),
                 Brand.Create(BrandId.Of(new Guid("b7bcda18-05cc-450b-b99a-3c5eafe111a5")), BrandName.Of("C&A")),
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
