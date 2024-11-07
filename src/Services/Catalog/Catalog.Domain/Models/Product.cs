using Catalog.Domain.Enums;
using Catalog.Domain.Events;
using Catalog.Domain.Models;
using Catalog.Domain.ValueObjects;

public class Product : Aggregate<ProductId>
{
    public IReadOnlyList<OccasionId> OccasionIds => _occasionIds.AsReadOnly();
    public IReadOnlyList<CategoryId> CategoryIds => _categoryIds.AsReadOnly();
    public IReadOnlyList<ColorVariant> ColorVariants => _colorVariants.AsReadOnly();
    public IReadOnlyList<Review> Reviews => _reviews.AsReadOnly();

    public ProductName Name { get; private set; } = default!;
    public UrlFriendlyName UrlFriendlyName { get; private set; } = default!;
    public ProductDescription Description { get; private set; } = default!;
    public AverageRating AverageRating { get; private set; } = default!;
    public Image CoverImage { get; private set; } = default!;
    public bool IsHandmade { get; private set; } = default!;
    public bool OnReorder { get; private set; } = default!;
    public ProductTypeId ProductTypeId { get; private set; } = default!;
    public ProductTypeEnum ProductType { get; private set; } = default!;
    public MaterialId MaterialId { get; private set; } = default!;
    public BrandId BrandId { get; private set; } = default!;
    public CollectionId CollectionId { get; private set; } = default!;
    private readonly List<Review> _reviews = new();
    private readonly List<OccasionId> _occasionIds = new();
    private readonly List<CategoryId> _categoryIds = new();
    private readonly List<ColorVariant> _colorVariants = new();

    private Product() { }
    private Product(
        ProductId id,
        ProductName name,
        UrlFriendlyName urlFriendlyName,
        ProductDescription description,
        bool isHandmade,
        bool onReorder,
        Image coverImage,
        ProductTypeId productTypeId,
        ProductTypeEnum productType,
        MaterialId materialId,
        BrandId brandId,
        CollectionId collectionId,
        AverageRating averageRating
        )
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        UrlFriendlyName = urlFriendlyName ?? throw new ArgumentNullException(nameof(urlFriendlyName));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        IsHandmade = isHandmade;
        OnReorder = onReorder;
        CoverImage = coverImage ?? throw new ArgumentNullException(nameof(coverImage));
        ProductTypeId = productTypeId ?? throw new ArgumentNullException(nameof(productTypeId));
        MaterialId = materialId ?? throw new ArgumentNullException(nameof(materialId));
        BrandId = brandId ?? throw new ArgumentNullException(nameof(brandId));
        CollectionId = collectionId ?? throw new ArgumentNullException(nameof(collectionId));
        AverageRating = averageRating ?? AverageRating.Of(0, 0);
        ProductType = productType;
    }

    // Méthode de création pour s'assurer que la création respecte la logique métier
    public static Product Create(
        ProductId id,
        ProductName name,
        UrlFriendlyName urlFriendlyName,
        ProductDescription description,
        bool isHandmade,
        bool onReorder,
        Image coverImage,
        ProductTypeId productTypeId,
        ProductTypeEnum productType,
        MaterialId materialId,
        BrandId brandId,
        CollectionId collectionId,
        AverageRating averageRating
        )
    {
        var product = new Product(
             id,
             name,
             urlFriendlyName,
             description,
             isHandmade,
             onReorder,
             coverImage,
             productTypeId,
             productType,
             materialId,
             brandId,
             collectionId,
             averageRating
         );

        product.AddDomainEvent(new ProductCreatedEvent(product));
        return product;
    }

    // Ajout d'une nouvelle évaluation (ProductReview)
    public void AddReview(Review review)
    {
        if (!_reviews.Contains(review))
        {
            _reviews.Add(review);
            //  AddDomainEvent(new ProductUpdatedEvent(this));
        }
    }

    // Méthode pour ajouter une occasion
    public void AddOccasion(OccasionId occasionId)
    {
        if (!_occasionIds.Contains(occasionId))
        {
            _occasionIds.Add(occasionId);
            //   AddDomainEvent(new ProductUpdatedEvent(this));
        }
    }

    // Méthode pour supoprimner une occasion
    public void RemoveOccasion(OccasionId occasionId)
    {
        if (_occasionIds.Contains(occasionId))
        {
            _occasionIds.Remove(occasionId);
            //    AddDomainEvent(new ProductUpdatedEvent(this));
        }
    }

    // Méthode pour ajouter une catégorie
    public void AddCategory(CategoryId categoryId)
    {
        if (!_categoryIds.Contains(categoryId))
        {
            _categoryIds.Add(categoryId);
            //   AddDomainEvent(new ProductUpdatedEvent(this));
        }
    }


    public void AddCategories(List<CategoryId> ids)
    {
        foreach (var id in ids)
        {
            AddCategory(id);
        }
    }

    public void AddOccasions(List<OccasionId> ids)
    {
        foreach (var id in ids)
        {
            AddOccasion(id);
        }
    }


    // Méthode pour supprimer une catégorie
    public void RemoveCategory(CategoryId categoryId)
    {
        if (_categoryIds.Contains(categoryId))
        {
            _categoryIds.Remove(categoryId);
            //  AddDomainEvent(new ProductUpdatedEvent(this));
        }
    }

    public void RemoveCategories(List<CategoryId> ids)
    {
        foreach (var id in ids)
        {
            RemoveCategory(id);
        }
    }


    public void RemoveOccasions(List<OccasionId> ids)
    {
        foreach (var id in ids)
        {
            RemoveOccasion(id);
        }
    }

    // Méthode pour ajouter une variante de couleur
    public void AddColorVariant(ColorVariant colorVariant)
    {
        if (!_colorVariants.Any(cv => cv.Color.Value.ToLower() == colorVariant.Color.Value.ToLower()))
        {
            _colorVariants.Add(colorVariant);
            //  AddDomainEvent(new ProductUpdatedEvent(this));
        }
    }

    // Méthode pour supprimer une variante de couleur
    public void RemoveColorVariant(ColorVariantId colorVariantId)
    {
        var colorVariant = _colorVariants.FirstOrDefault(cv => cv.Id == colorVariantId);
        if (colorVariant != null)
        {
            _colorVariants.Remove(colorVariant);
            // AddDomainEvent(new ProductUpdatedEvent(this));
        }
    }

    // Méthode pour mettre à jour la note moyenne après une nouvelle évaluation
    public void UpdateAverageRating(decimal newRating)
    {
        AverageRating = AverageRating.AddNewRating(newRating);
        //AddDomainEvent(new ProductUpdatedEvent(this));
    }

    // Méthodes de mise à jour des informations sur le produit
    public void UpdateDescription(ProductDescription newDescription)
    {
        if (Description == newDescription)
            return;
        Description = newDescription ?? throw new ArgumentNullException(nameof(newDescription));
    }

    public void UpdateCoverImage(Image coverImage)
    {
        if (CoverImage.ImageSrc == coverImage.ImageSrc)
            return;
        CoverImage = coverImage;
    }

    public void UpdateNames(ProductName newName, UrlFriendlyName urlFriendlyName)
    {
        if (newName == Name && urlFriendlyName == UrlFriendlyName)
            return;
        Name = newName ?? throw new ArgumentNullException(nameof(newName));
        UrlFriendlyName = urlFriendlyName ?? throw new ArgumentNullException(nameof(urlFriendlyName));
    }

    public void UpdateHandMade(bool isHandmade)
    {
        if (IsHandmade == isHandmade)
            return;
        IsHandmade = isHandmade;
    }

    public void UpdateMaterial(MaterialId materialId)
    {
        if (materialId == MaterialId)
            return;
        MaterialId = materialId ?? throw new ArgumentNullException(nameof(materialId));
    }
    public void UpdateCollection(CollectionId collectionId)
    {
        if (CollectionId == collectionId)
            return;
        CollectionId = collectionId ?? throw new ArgumentNullException(nameof(collectionId));
    }
    public void UpdateBrand(BrandId brandId)
    {
        if (BrandId == brandId)
            return;
        BrandId = brandId ?? throw new ArgumentNullException(nameof(brandId));
    }
    public void ToogleOnReorder()
    {
        OnReorder = !OnReorder;
    }
}
