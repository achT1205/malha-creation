//using BuildingBlocks.Enums;

//namespace Cart.API.Models;

//public class Product
//{
//    public Guid Id { get; set; }  // Identifiant unique du produit
//    public string Name { get; set; } = default!; // Nom du produit
//    public ProductTypeEnum ProductType { get; set; } // Type de produit (Clothing, Accessory)
//    public List<ColorVariant> ColorVariants { get; set; } = new();  // Redéfinit les variantes pour avoir des prix directs
//}


//public class ColorVariant
//{
//    public string Slug { get; set; } = default!; // Identifiant "slug" pour l'URL
//    public string Color { get; set; } = default!; // Nom ou identifiant de la couleur
//    public decimal? Price { get; set; }  // Prix pour cette couleur
//    public int? Quantity { get; set; }  // Quantité disponible pour cette couleur
//    public List<SizeVariant>? Sizes { get; set; } = new(); // Liste des tailles disponibles pour cette couleur
//}

//public class SizeVariant
//{
//    public string Size { get; set; } = default!; // Taille (e.g., S, M, L)
//    public decimal Price { get; set; }  // Prix pour cette taille
//    public int Quantity { get; set; }  // Quantité disponible pour cette taille
//}


using System.Text.Json.Serialization;

public class Product
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("urlFriendlyName")]
    public string UrlFriendlyName { get; set; } = default!;

    [JsonPropertyName("description")]
    public string Description { get; set; } = default!;

    [JsonPropertyName("isHandmade")]
    public bool IsHandmade { get; set; }

    [JsonPropertyName("coverImage")]
    public CoverImage CoverImage { get; set; } = default!;

    [JsonPropertyName("productTypeId")]
    public Guid ProductTypeId { get; set; }

    [JsonPropertyName("materialId")]
    public Guid MaterialId { get; set; }

    [JsonPropertyName("collectionId")]
    public Guid CollectionId { get; set; }

    [JsonPropertyName("occasionIds")]
    public List<Guid> OccasionIds { get; set; } = default!;

    [JsonPropertyName("categoryIds")]
    public List<Guid> CategoryIds { get; set; } = new List<Guid>();

    [JsonPropertyName("colorVariants")]
    public List<ColorVariant> ColorVariants { get; set; } = new();

    [JsonPropertyName("productType")]
    public string ProductType { get; set; } = default!;

    [JsonPropertyName("material")]
    public string Material { get; set; } = default!;

    [JsonPropertyName("collection")]
    public string Collection { get; set; } = default!;

    [JsonPropertyName("occasions")]
    public List<string> Occasions { get; set; } = new();

    [JsonPropertyName("categories")]
    public List<string> Categories { get; set; } = new();
}

public class CoverImage
{
    [JsonPropertyName("imageSrc")]
    public string ImageSrc { get; set; } = default!;

    [JsonPropertyName("altText")]
    public string AltText { get; set; } = default!;
}

public class ColorVariant
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("color")]
    public string Color { get; set; } = default!;

    [JsonPropertyName("images")]
    public List<Image> Images { get; set; } = default!;

    [JsonPropertyName("price")]
    public Price Price { get; set; } = default!;

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("slug")]
    public string Slug { get; set; } = default!;

    [JsonPropertyName("sizeVariants")]
    public List<SizeVariant> SizeVariants { get; set; } = default!;
}

public class Image
{
    [JsonPropertyName("imageSrc")]
    public string ImageSrc { get; set; } = default!;

    [JsonPropertyName("altText")]
    public string AltText { get; set; } = default!;
}

public class Price
{
    [JsonPropertyName("currency")]
    public string Currency { get; set; } = default!;

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }
}

public class SizeVariant
{
    [JsonPropertyName("size")]
    public string Size { get; set; } = default!;

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; } = default!;

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
}
