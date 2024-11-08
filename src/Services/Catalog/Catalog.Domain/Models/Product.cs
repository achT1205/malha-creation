using Catalog.Domain.Enums;
using Catalog.Domain.Events;

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
        CoverImage = coverImage ?? throw new ArgumentNullException(nameof(coverImage));
        ProductTypeId = productTypeId ?? throw new ArgumentNullException(nameof(productTypeId));
        MaterialId = materialId ?? throw new ArgumentNullException(nameof(materialId));
        BrandId = brandId ?? throw new ArgumentNullException(nameof(brandId));
        CollectionId = collectionId ?? throw new ArgumentNullException(nameof(collectionId));
        AverageRating = averageRating ?? AverageRating.Of(0, 0);
        ProductType = productType;
    }
    public static Product Create(
        ProductId id,
        ProductName name,
        UrlFriendlyName urlFriendlyName,
        ProductDescription description,
        bool isHandmade,
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
             coverImage,
             productTypeId,
             productType,
             materialId,
             brandId,
             collectionId,
             averageRating
         );

        product.AddDomainEvent(new ProductCreatedEventDomainEvent(product));
        return product;
    }

    public void AddReview(Review review)
    {
        if (!_reviews.Contains(review))
        {
            _reviews.Add(review);
        }
    }

    public void AddOccasion(OccasionId occasionId)
    {
        if (!_occasionIds.Contains(occasionId))
        {
            _occasionIds.Add(occasionId);
        }
    }

    public void RemoveOccasion(OccasionId occasionId)
    {
        if (_occasionIds.Contains(occasionId))
        {
            _occasionIds.Remove(occasionId);
        }
    }

    public void AddCategory(CategoryId categoryId)
    {
        if (!_categoryIds.Contains(categoryId))
        {
            _categoryIds.Add(categoryId);
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

    public void RemoveCategory(CategoryId categoryId)
    {
        if (_categoryIds.Contains(categoryId))
        {
            _categoryIds.Remove(categoryId);
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

    public void AddColorVariant(ColorVariant colorVariant)
    {
        if (!_colorVariants.Any(cv => cv.Color.Value.ToLower() == colorVariant.Color.Value.ToLower()))
        {
            _colorVariants.Add(colorVariant);
            AddDomainEvent(new ProductNewColorVariantAddedDomainEvent(colorVariant));
        }
    }
    public void RemoveColorVariant(ColorVariantId colorVariantId)
    {
        var colorVariant = _colorVariants.FirstOrDefault(cv => cv.Id == colorVariantId);
        if (colorVariant != null)
        {
            if (colorVariant.OnOrdering)
            {
                throw new CatalogDomainException($"This colorVariant is on ordering, can not remove it.");
            }
            _colorVariants.Remove(colorVariant);
            AddDomainEvent(new ProductColorVariantRemovedDomainEvent(colorVariant));
        }
    }

    public void AddColorVariantStock(ColorVariantId colorVariantId, ColorVariantQuantity quantity)
    {
        var cv = ColorVariants.FirstOrDefault(_ => _.Equals(colorVariantId));
        if (cv == null)
        {
            throw new CatalogDomainException($"The ColorVariant {colorVariantId} was not found");
        }
        if (ProductType == ProductTypeEnum.Clothing)
        {
            throw new CatalogDomainException($"Can add stock only to size variant");
        }
        cv.AddStock(quantity.Value.Value);

        AddDomainEvent(new ColorVariantStockAddedDomainEvent(colorVariantId.Value, cv.Quantity.Value.Value));
    }

    public void AddSizeVariantStock(ColorVariantId colorVariantId, SizeVariantId sizeVariantId, Quantity quantity)
    {
        var cv = ColorVariants.FirstOrDefault(_ => _.Equals(colorVariantId));
        if (cv == null)
        {
            throw new CatalogDomainException($"The ColorVariant {colorVariantId} was not found");
        }
        var sv = cv.SizeVariants.FirstOrDefault(sv => sv.Id == sizeVariantId);
        if (sv == null)
        {
            throw new CatalogDomainException($"The SizeVariant {sizeVariantId} was not found");
        }
        sv.AddStock(quantity.Value);
        AddDomainEvent(new SizeVariantStockAddedDomainEvent(colorVariantId.Value, sizeVariantId.Value, sv.Quantity.Value));
    }

    public void UpdateAverageRating(decimal newRating)
    {
        AverageRating = AverageRating.AddNewRating(newRating);
    }

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

    public void AddSizeVariant(ColorVariantId colorVariantId, SizeVariant sizeVariant)
    {
        var cv = ColorVariants.FirstOrDefault(_ => _.Equals(colorVariantId));
        if (cv == null)
        {
            throw new CatalogDomainException($"The ColorVariant {colorVariantId} was not found");
        }
        cv.AddSizeVariant(sizeVariant);
    }

    public void RemoveSizeVariant(ColorVariantId colorVariantId, SizeVariantId sizeVariantId)
    {
        var cv = ColorVariants.FirstOrDefault(_ => _.Equals(colorVariantId));
        if (cv == null)
        {
            throw new CatalogDomainException($"The ColorVariant {colorVariantId} was not found");
        }
        cv.RemoveSizeVariant(sizeVariantId);
    }

    public void UpdateColorVariantPrice(ColorVariantId colorVariantId, ColorVariantPrice price)
    {
        if (ProductType == ProductTypeEnum.Clothing)
        {
            throw new CatalogDomainException($"Only Size varaiant price can be changed for this product.");
        }
        var cv = ColorVariants.FirstOrDefault(_ => _.Equals(colorVariantId));
        if (cv == null)
        {
            throw new CatalogDomainException($"The ColorVariant {colorVariantId} was not found");
        }
        var oldPrice = cv.Price;
        cv.UpdatePrice(price);
        AddDomainEvent(new ProductColorVariantPriceChangedDomainEvent(Id.Value, colorVariantId.Value, price.Amount.Value, oldPrice.Amount.Value, price.Currency));
    }

    public void ToogleOnReorder()
    {
        OnReorder = !OnReorder;
    }
}
