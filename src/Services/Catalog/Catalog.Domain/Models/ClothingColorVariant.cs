
namespace Catalog.Domain.Models;

public class ClothingColorVariant : ColorVariant
{
    private readonly List<SizeVariant> _sizeVariants = new();
    public IReadOnlyList<SizeVariant> SizeVariants => _sizeVariants.AsReadOnly();

    private ClothingColorVariant()
    {
        
    }
    public ClothingColorVariant(ProductId productId, Color color, Slug slug)
        : base(productId, color, slug)
    {

    }

    public static ClothingColorVariant Create(ProductId productId, Color color, Slug slug)
    {
        return new ClothingColorVariant(productId, color, slug);
    }

    public void AddSizeVariant(SizeVariant sizeVariant)
    {
        if (sizeVariant == null)
        {
            throw new ArgumentNullException(nameof(sizeVariant));
        }
        var existingVariant = SizeVariants.FirstOrDefault(sv => sv.Size.Equals(sizeVariant));

        if (existingVariant != null)
        {
            throw new InvalidOperationException("A size variant with the same ID already exists.");
        }

        _sizeVariants.Add(sizeVariant);
    }

    public override void AddImage(Image image)
    {
        if (_images.Contains(image))
            return;

        _images.Add(image);
    }
}
