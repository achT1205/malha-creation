using Catalog.Domain.Abstractions;
using Catalog.Domain.ValueObjects;

namespace Catalog.Domain.Models;

public class Product : Entity<ProductId>
{
    public string Name { get; private set; } = default!;
    public string NameEn { get; private set; } = default!;
    public string CoverImage { get; private set; } = default!;
    public ProductType ProductType { get; private set; } = default!;
    public Occasion Occasion { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public Material Material { get; private set; } = default!;
    public bool IsHandmade { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; }
    public CollectionId CollectionId { get; private set; }
    public List<Category> Categories { get; private set; } = new();
    public List<ColorVariant> ColorVariants { get; private set; } = new();

    // Méthodes pour gérer les invariants métier
    public void UpdateName(string newName)
    {
        Name = newName ?? throw new ArgumentNullException(nameof(newName));
        UpdatedAt = DateTime.UtcNow;
    }

    // Autres méthodes business pour le produit
}
