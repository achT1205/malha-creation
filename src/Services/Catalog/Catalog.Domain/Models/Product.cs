public abstract class Product<T> : Aggregate<ProductId>
{
    public IReadOnlyList<OccasionId> OccasionIds => _occasionIds.AsReadOnly();
    public IReadOnlyList<CategoryId> CategoryIds => _categoryIds.AsReadOnly();
    public IReadOnlyList<ProductReviewId>  ProductReviewIds => _productReviewIds.AsReadOnly();
    public IReadOnlyList<T> ColorVariants => _colorVariants.AsReadOnly();
    protected readonly List<T> _colorVariants = new();

    public ProductName Name { get; protected set; } = default!;
    public UrlFriendlyName UrlFriendlyName { get; protected set; } = default!;
    public ProductDescription Description { get; protected set; } = default!;
    public AverageRating AverageRating { get; protected set; } = default!;
    public Image CoverImage { get; protected set; } = default!; 
    public bool IsHandmade { get; protected set; } = default!;
    public ProductTypeId ProductTypeId { get; protected set; } = default!;
    public MaterialId MaterialId { get; protected set; } = default!;
    public CollectionId CollectionId { get; protected set; } = default!;
    protected readonly List<ProductReviewId>  _productReviewIds = new();
    protected readonly List<OccasionId> _occasionIds = new();
    protected readonly List<CategoryId> _categoryIds = new();

    protected Product() { }
    protected Product(
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

    public abstract void AddReview(ProductReviewId reviewId);
    public abstract void AddOccasion(OccasionId occasionId);
    public abstract void AddCategory(CategoryId categoryId);
    public abstract void UpdateAverageRating(decimal newRating);
    public abstract void AddColorVariant(T colorVariant);
}
