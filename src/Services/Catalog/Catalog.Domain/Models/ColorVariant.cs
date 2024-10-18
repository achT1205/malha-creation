namespace Catalog.Domain.Models;

public class ColorVariant : Entity<ColorVariantId>
{
    public ProductId ProductId { get; private set; } = default!;
    public Color Color { get; protected set; } = default!;
    public Slug Slug { get; protected set; } = default!;

    private readonly List<Image> _images = new();
    public IReadOnlyList<Image>  Images => _images.AsReadOnly();

    //private readonly List<SizeVariant> _sizeVariants = new();
    //public IReadOnlyList<SizeVariant> SizeVariants => _sizeVariants.AsReadOnly();

    public Price Price { get; private set; } = default!;
    public Quantity Quantity { get; private set; } = default!;

    private ColorVariant()
    {
        
    }
    private ColorVariant(ProductId productId, Color color, Slug slug, Price price, Quantity quantity)
    {
        Id = ColorVariantId.Of(Guid.NewGuid());
        Price = price ;
        Quantity = quantity ;
        ProductId = productId;
        Color = color ?? throw new ArgumentNullException(nameof(color));
        Slug = slug ?? throw new ArgumentNullException(nameof(slug));
    }

    public static ColorVariant Create(ProductId productId, Color color, Slug slug, Price price, Quantity quantity)
    {
        return new ColorVariant(productId, color, slug, price, quantity);
    }

    public void UpdatePrice(Price newPrice)
    {
        if (!Price.Equals(newPrice))
        {
            Price = newPrice ?? throw new ArgumentNullException(nameof(newPrice));
        }
    }

    public void UpdateQuantity(Quantity newQuantity)
    {
        if (!Quantity.Equals(newQuantity))
        {
            Quantity = newQuantity ?? throw new ArgumentNullException(nameof(newQuantity));
        }
    }

    //public void AddSizeVariant(SizeVariant sizeVariant)
    //{
    //    if (sizeVariant == null)
    //    {
    //        throw new ArgumentNullException(nameof(sizeVariant));
    //    }

    //    // Vérification pour éviter les doublons
    //    var existingVariant = SizeVariants.FirstOrDefault(sv => sv.Size.Equals(sizeVariant));

    //    if (existingVariant != null)
    //    {
    //        throw new InvalidOperationException("A size variant with the same ID already exists.");
    //    }

    //    _sizeVariants.Add(sizeVariant);
    //}

    public void AddImage(Image  image)
    {
        if (_images.Contains(image))
            return;

        _images.Add(image);
    }



    // Method to update the quantity of a specific SizeVariant
    //public void UpdateSizeVariantQuantity(Size size, Quantity newQuantity)
    //{
    //    var existingVariant = SizeVariants.FirstOrDefault(sv => sv.Size.Equals(size));

    //    if (existingVariant != null)
    //    {
    //        var updatedVariant = SizeVariant.Create(existingVariant.Size, existingVariant.Price, newQuantity);
    //        ReplaceSizeVariant(existingVariant, updatedVariant);
    //    }
    //    else
    //    {
    //        throw new InvalidOperationException("Size variant not found.");
    //    }
    //}


    //public void UpdateSizeVariantPrice(Size size, Price newPrice)
    //{
    //    var existingVariant = SizeVariants.FirstOrDefault(sv => sv.Size.Equals(size));

    //    if (existingVariant != null)
    //    {
    //        var updatedVariant = SizeVariant.Create(existingVariant.Size, newPrice, existingVariant.Quantity);
    //        ReplaceSizeVariant(existingVariant, updatedVariant);
    //    }
    //    else
    //    {
    //        throw new InvalidOperationException("Size variant not found.");
    //    }
    //}

    //// Helper method to replace the old SizeVariant with the new one
    //private void ReplaceSizeVariant(SizeVariant oldVariant, SizeVariant newVariant)
    //{
    //    var index = _sizeVariants.IndexOf(oldVariant);
    //    if (index >= 0)
    //    {
    //        _sizeVariants[index] = newVariant;
    //    }
    //}
}
