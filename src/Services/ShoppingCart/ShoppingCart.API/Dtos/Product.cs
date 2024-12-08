using ShoppingCart.API.Enums;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string UrlFriendlyName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool IsHandmade { get; set; }
    public CoverImage CoverImage { get; set; } = default!;
    public ProductType ProductType { get; set; } = default!;
    public Material Material { get; set; } = default!;
    public Collection Collection { get; set; } = default!;
    public Brand Brand { get; set; } = default!;
    public List<ColorVariant> ColorVariants { get; set; } = default!;
    public List<Occasion> Occasions { get; set; } = default!;
    public List<Category> Categories { get; set; } = default!;
}

public class CoverImage
{
    public string ImageSrc { get; set; } = default!;
    public string AltText { get; set; } = default!;
}

//public class ProductType
//{
//    public Guid Id { get; set; }
//    public string Name { get; set; } = default!;
//}

public class Material
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}

public class Collection
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string ImageSrc { get; set; } = default!;
    public string AltText { get; set; } = default!;
}

public class Brand
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}

public class ColorVariant
{
    public Guid Id { get; set; }
    public string Color { get; set; } = default!;
    public List<Image> Images { get; set; } = default!;
    public Price Price { get; set; } = default!;
    public int? Quantity { get; set; }
    public int? RestockThreshold { get; set; }
    public string Slug { get; set; } = default!;
    public List<SizeVariant> SizeVariants { get; set; } = default!;
}

public class Image
{
    public string ImageSrc { get; set; } = default!;
    public string AltText { get; set; } = default!;
}

public class Price
{
    public string Currency { get; set; } = default!;
    public decimal? Amount { get; set; }
}

public class SizeVariant
{
    public Guid Id { get; set; }
    public string Size { get; set; } = default!;
    public decimal Price { get; set; }
    public string Currency { get; set; } = default!;
    public int Quantity { get; set; }
    public int RestockThreshold { get; set; }
}

public class Occasion
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}