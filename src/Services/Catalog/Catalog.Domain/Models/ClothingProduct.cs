using Catalog.Domain.Enums;
using Catalog.Domain.Events;

namespace Catalog.Domain.Models;
public class ClothingProduct : Product
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
        ProductTypeEnum productTypeEnum,
        MaterialId materialId,
        CollectionId collectionId,
        AverageRating averageRating
        ) : base(id, name, urlFriendlyName, description, isHandmade, coverImage, productTypeId, productTypeEnum, materialId, collectionId, averageRating)
        {

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
        ProductTypeEnum productTypeEnum,
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
             productTypeEnum,
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
    public void AddColorVariant(ClothingColorVariant colorVariant)
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

    public override void AddColorVariant(ColorVariant colorVariant)
    {
        throw new NotImplementedException();
    }
}
