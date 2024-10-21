using Catalog.Domain.Events;

namespace Catalog.Domain.Models;
public class ClothingProduct : Product<ClothingColorVariant>
{
    private ClothingProduct() { }
    private ClothingProduct(
        ProductId id,
        ProductName name,
        UrlFriendlyName urlFriendlyName,
        ProductDescription description,
        bool isHandmade,
        Image coverImage,
        ProductTypeId productTypeId,
        MaterialId materialId,
        CollectionId collectionId,
        AverageRating averageRating
        ) : base(id, name, urlFriendlyName, description, isHandmade, coverImage, productTypeId, materialId, collectionId, averageRating)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        UrlFriendlyName = urlFriendlyName ?? throw new ArgumentNullException(nameof(urlFriendlyName));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        IsHandmade = isHandmade;
        CoverImage = coverImage ?? throw new ArgumentNullException(nameof(coverImage));
        ProductTypeId = productTypeId ?? throw new ArgumentNullException(nameof(productTypeId));
        MaterialId = materialId ?? throw new ArgumentNullException(nameof(materialId));
        CollectionId = collectionId ?? throw new ArgumentNullException(nameof(collectionId));
        AverageRating = averageRating ?? AverageRating.Of(0, 0);
    }

    // Méthode de création pour s'assurer que la création respecte la logique métier
    public static ClothingProduct Create(
        ProductId id,
        ProductName name,
        UrlFriendlyName urlFriendlyName,
        ProductDescription description,
        bool isHandmade,
        Image coverImage,
        ProductTypeId productTypeId,
        MaterialId materialId,
        CollectionId collectionId,
        AverageRating averageRating
        )
    {
        var product = new ClothingProduct(
             id,
             name,
             urlFriendlyName,
             description,
             isHandmade,
             coverImage,
             productTypeId,
             materialId,
             collectionId,
             averageRating
         );

        product.AddDomainEvent(new ClothingProductCreatedEvent(product));
        return product;
    }

    // Ajout d'une nouvelle évaluation (ProductReview)
    public override void AddReview(ProductReviewId reviewId)
    {
        if (!_productReviewIds.Contains(reviewId))
        {
            _productReviewIds.Add(reviewId);
            AddDomainEvent(new ClothingProductUpdatedEvent(this));
        }
    }

    // Méthode pour ajouter une occasion
    public override void AddOccasion(OccasionId occasionId)
    {
        if (!_occasionIds.Contains(occasionId))
        {
            _occasionIds.Add(occasionId);
            AddDomainEvent(new ClothingProductUpdatedEvent(this));
        }
    }

    // Méthode pour ajouter une catégorie
    public override void AddCategory(CategoryId categoryId)
    {
        if (!_categoryIds.Contains(categoryId))
        {
            _categoryIds.Add(categoryId);
            AddDomainEvent(new ClothingProductUpdatedEvent(this));
        }
    }

    // Méthode pour ajouter une variante de couleur
    public override void AddColorVariant(ClothingColorVariant colorVariant)
    {
        if (!_colorVariants.Contains(colorVariant))
        {
            _colorVariants.Add(colorVariant);
            AddDomainEvent(new ClothingProductUpdatedEvent(this));
        }
    }

    // Méthode pour mettre à jour la note moyenne après une nouvelle évaluation
    public override void UpdateAverageRating(decimal newRating)
    {
        AverageRating = AverageRating.AddNewRating(newRating);
        AddDomainEvent(new ClothingProductUpdatedEvent(this));
    }
}
