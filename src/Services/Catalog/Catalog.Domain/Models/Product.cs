using Catalog.Domain.Events;

public class Product : Aggregate<ProductId>
{
    public string Name { get; private set; }
    public string UrlFriendlyName { get; private set; }
    public string Description { get; private set; }
    public Image CoverImage { get; set; }
    public bool IsHandmade { get; private set; }
    public ProductType ProductType { get; private set; }
    public Material Material { get; private set; }
    public Collection Collection { get; private set; }
    public List<Category> Categories { get; private set; }
    public List<Occasion> Occasions { get; private set; }
    public List<ColorVariantBase> ColorVariants { get; private set; }

    private Product(
        ProductId id,
        string name,
        string urlFriendlyName,
        string description,
        bool isHandmade,
        Image coverImage,
        ProductType productType,
        Material material,
        Collection collection,
        List<Occasion> occasions,
        List<Category> categories
        )
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        UrlFriendlyName = urlFriendlyName ?? throw new ArgumentNullException(nameof(urlFriendlyName));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        IsHandmade = isHandmade;
        CoverImage = coverImage ?? throw new ArgumentNullException(nameof(coverImage));
        ProductType = productType ?? throw new ArgumentNullException(nameof(productType));
        Material = material ?? throw new ArgumentNullException(nameof(material));
        Collection = collection ?? throw new ArgumentNullException(nameof(collection));
        Categories = categories ?? new List<Category>();
        Occasions = occasions ?? new List<Occasion>();
        ColorVariants = new List<ColorVariantBase>();
    }

    public static Product Create(
        string name,
        string UrlFriendlyName,
        string description,
        bool isHandmade,
        Image coverImage,
        ProductType productType,
        Occasion occasion,
        Material material,
        Collection collection,
        List<Occasion> occasions,
        List<Category> categories
        )
    {
        var product = new Product(
             ProductId.Of(Guid.NewGuid()),
             name,
             UrlFriendlyName,
             description,
             isHandmade,
             coverImage,
             productType,
             material,
             collection,
             occasions,
             categories
         );

        product.AddDomainEvent(new ProductCreatedEvent(product));
        return product;
    }

    // Ajout d'une variante de couleur
    public void AddColorVariant(ColorVariantBase colorVariant)
    {
        if (colorVariant == null)
        {
            throw new ArgumentNullException(nameof(colorVariant));
        }
        if (ColorVariants.Any(c => c.Slug.Value == colorVariant.Slug.Value))
        {
            throw new InvalidOperationException("A color variant with the same slug already exists.");
        }

        ColorVariants.Add(colorVariant);
        AddDomainEvent(new ProductUpdatedEvent(this));
    }

    public void RemoveColorVariant(ColorVariantId colorVariantId)
    {
        var colorVariant = ColorVariants.FirstOrDefault(c => c.Id == colorVariantId);
        if (colorVariant != null)
        {
            ColorVariants.Remove(colorVariant);
            AddDomainEvent(new ProductUpdatedEvent(this));
        }
        else
        {
            throw new InvalidOperationException("Color variant not found.");
        }
    }

    // Méthodes pour ajouter des catégories
    public void AddCategory(Category category)
    {
        if (category == null)
        {
            throw new ArgumentNullException(nameof(category));
        }

        if (!Categories.Contains(category))
        {
            Categories.Add(category);
            AddDomainEvent(new ProductUpdatedEvent(this));
        }
    }

    // Suppression d'une catégorie
    public void RemoveCategory(CategoryId categoryId)
    {
        var category = Categories.FirstOrDefault(c => c.Id == categoryId);
        if (category != null)
        {
            Categories.Remove(category);
            AddDomainEvent(new ProductUpdatedEvent(this));
        }
        else
        {
            throw new InvalidOperationException("Color variant not found.");
        }
    }

    // Modification de la collection
    public void UpdateCollection(Collection newCollection)
    {
        if (!Collection.Equals(newCollection))
        {
            Collection = newCollection ?? throw new ArgumentNullException(nameof(newCollection));
            AddDomainEvent(new ProductUpdatedEvent(this));
        }
    }

    // Modification du matériau
    public void UpdateMaterial(Material newMaterial)
    {
        if (!Material.Equals(newMaterial))
        {
            Material = newMaterial ?? throw new ArgumentNullException(nameof(newMaterial));
            AddDomainEvent(new ProductUpdatedEvent(this));
        }
    }


    // Méthodes de mise à jour des informations sur le produit
    public void UpdateDescription(string newDescription)
    {
        Description = newDescription ?? throw new ArgumentNullException(nameof(newDescription));
        AddDomainEvent(new ProductUpdatedEvent(this));
    }

    public void UpdateName(string newName, string newUrlFriendlyName)
    {
        Name = newName ?? throw new ArgumentNullException(nameof(newName));
        UrlFriendlyName = newUrlFriendlyName ?? throw new ArgumentNullException(nameof(newUrlFriendlyName));
        AddDomainEvent(new ProductUpdatedEvent(this));
    }
}