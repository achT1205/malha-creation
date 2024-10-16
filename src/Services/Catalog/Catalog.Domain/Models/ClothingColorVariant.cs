namespace Catalog.Domain.Models;

public class ClothingColorVariant : ColorVariantBase
{
    public List<SizeVariant>  SizeVariants { get; private set; }

    private ClothingColorVariant(ColorVariantId id, Color color, Slug slug, List<Image> images, List<SizeVariant> sizeVariants)
        : base(id, color, slug, images)
    {
        SizeVariants = sizeVariants;
    }

    public static ClothingColorVariant Create(Color color, Slug slug, List<Image> images, List<SizeVariant> sizeVariants)
    {
        return new ClothingColorVariant(ColorVariantId.Of(Guid.NewGuid()), color, slug, images, sizeVariants);
    }

    public override void AddSizeVariant(SizeVariant sizeVariant)
    {
        if (sizeVariant == null)
        {
            throw new ArgumentNullException(nameof(sizeVariant));
        }

        // Vérification pour éviter les doublons
        var existingVariant = SizeVariants.FirstOrDefault(sv => sv.Size.Equals(sizeVariant));

        if (existingVariant != null)
        {
            throw new InvalidOperationException("A size variant with the same ID already exists.");
        }

        SizeVariants.Add(sizeVariant);
    }


    // Method to update the quantity of a specific SizeVariant
    public void UpdateSizeVariantQuantity(Size size, Quantity newQuantity)
    {
        var existingVariant = SizeVariants.FirstOrDefault(sv => sv.Size.Equals(size));

        if (existingVariant != null)
        {
            var updatedVariant = SizeVariant.Create(existingVariant.Size, existingVariant.Price, newQuantity);
            ReplaceSizeVariant(existingVariant, updatedVariant);
        }
        else
        {
            throw new InvalidOperationException("Size variant not found.");
        }
    }


    public void UpdateSizeVariantPrice(Size size, Price newPrice)
    {
        var existingVariant = SizeVariants.FirstOrDefault(sv => sv.Size.Equals(size));

        if (existingVariant != null)
        {
            var updatedVariant = SizeVariant.Create(existingVariant.Size, newPrice, existingVariant.Quantity);
            ReplaceSizeVariant(existingVariant, updatedVariant);
        }
        else
        {
            throw new InvalidOperationException("Size variant not found.");
        }
    }

    // Helper method to replace the old SizeVariant with the new one
    private void ReplaceSizeVariant(SizeVariant oldVariant, SizeVariant newVariant)
    {
        var index = SizeVariants.IndexOf(oldVariant);
        if (index >= 0)
        {
            SizeVariants[index] = newVariant;
        }
    }
    public override IReadOnlyCollection<SizeVariant> GetSizes() => SizeVariants.AsReadOnly();

}

