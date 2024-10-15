using Catalog.Domain.Events;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;

namespace Catalog.Domain.Models;

public class Product : Aggregate<ProductId>
{
    private readonly List<ColorVariant> _colorVariants = new();
    public IReadOnlyList<ColorVariant> ColorVariants => _colorVariants.AsReadOnly();

    private readonly List<OccasionId> _occasionIds = new();
    public IReadOnlyList<OccasionId> OccasionIds => _occasionIds.AsReadOnly();

    private readonly List<CategoryId> _categoryIds = new();
    public IReadOnlyList<CategoryId> CategoryIds => _categoryIds.AsReadOnly();

    public string Name { get; private set; } = default!;
    public string NameEn { get; private set; } = default!;
    public ImageId CoverImageId { get; private set; } = default!;
    public ProductTypeId ProductTypeId { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public MaterialId MaterialId { get; private set; } = default!;
    public bool IsHandmade { get; private set; }
    public CollectionId CollectionId { get; private set; } = default!;

    public static Product Create(
        ProductId productId,
        string name,
        string nameEn,
        ImageId coverImageId,
        ProductTypeId productTypeId,
        string description,
        MaterialId materialId,
        bool isHandmade,
        CollectionId collectionId

    )
    {
        var product = new Product()
        {
            Id = productId,
            Name = name ?? throw new ArgumentNullException(nameof(name)),
            NameEn = nameEn ?? throw new ArgumentNullException(nameof(nameEn)),
            CoverImageId = coverImageId,
            ProductTypeId = productTypeId,
            MaterialId = materialId,
            IsHandmade = isHandmade,
            CollectionId = collectionId,
            Description = description ?? throw new ArgumentNullException(nameof(description))
        };

        product.AddDomainEvent(new ProductCreatedEvent(product));
        return product;
    }


    public void Update(
        string name,
        string nameEn,
        ImageId coverImageId,
        MaterialId materialId,
        bool isHandmade,
        CollectionId collectionId)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        NameEn = nameEn ?? throw new ArgumentNullException(nameof(nameEn));
        CoverImageId = coverImageId;
        MaterialId = materialId;
        IsHandmade = isHandmade;
        CollectionId = collectionId;
        LastModified = DateTime.UtcNow;
        AddDomainEvent(new ProductUpdatedEvent(this));
    }

    public void AddColorVariant(string color, List<string> images, Price? price)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(color);
        var slug = GenerateSlug(NameEn, color);
        var variant = new ColorVariant(Id, color, images, slug, price);

        _colorVariants.Add(variant);

        AddDomainEvent(new ProductUpdatedEvent(this));
        AddDomainEvent(new ProductVariantAddedEvent(variant));
    }

    public void RemoveColorVariant(ColorVariantId colorVariantId)
    {
        var variant = _colorVariants.FirstOrDefault(x => x.Id == colorVariantId);
        if (variant is not null)
        {
            _colorVariants.Remove(variant);
            AddDomainEvent(new ProductUpdatedEvent(this));
            AddDomainEvent(new ProductVariantRemovedEvent(variant));
        }
    }


    public void AddOccasion(OccasionId occasionId)
    {
        if (!_occasionIds.Contains(occasionId))
        {
            _occasionIds.Add(occasionId);
            AddDomainEvent(new ProductUpdatedEvent(this));
        }
    }

    public void RemoveOccasion(OccasionId occasionId)
    {
        if (_occasionIds.Contains(occasionId))
        {
            _occasionIds.Remove(occasionId);
            AddDomainEvent(new ProductUpdatedEvent(this));
        }
    }


    public void AddCategory(CategoryId  categoryId)
    {
        if (!_categoryIds.Contains(categoryId))
        {
            _categoryIds.Add(categoryId);
            AddDomainEvent(new ProductUpdatedEvent(this));
        }
    }

    public void RemoveCategory(CategoryId categoryId)
    {
        if (_categoryIds.Contains(categoryId))
        {
            _categoryIds.Remove(categoryId);
            AddDomainEvent(new ProductUpdatedEvent(this));
        }
    }



    private string GenerateSlug(string productName, string color)
    {
        // Concatenate product name and color
        string slugInput = $"{productName} in {color}";

        // Convert to lowercase
        slugInput = slugInput.ToLowerInvariant();

        // Remove all invalid characters
        slugInput = RemoveDiacritics(slugInput); // Optional: Normalize and remove accents/diacritics
        slugInput = Regex.Replace(slugInput, @"[^a-z0-9\s-]", "");  // Keep only alphanumeric, spaces, and hyphens

        // Replace spaces with hyphens
        slugInput = Regex.Replace(slugInput, @"\s+", "-").Trim();

        // Optionally, truncate the slug to a reasonable length
        slugInput = slugInput.Substring(0, slugInput.Length <= 100 ? slugInput.Length : 100).Trim('-');

        return slugInput;
    }

    // Optional: Remove diacritics (accents on letters like é, à, etc.)
    private string RemoveDiacritics(string text)
    {
        var normalizedString = text.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }
}
