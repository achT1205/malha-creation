using BuildingBlocks.Messaging.Events;
using Catalog.API.Helpers;

namespace Catalog.API.Products.Commands.CreateProduct;
public record CreateProductCommand
    : ICommand<CreateProductResult>
{
    public Guid Id { get; set; }  // Identifiant unique du produit
    public string Name { get; set; } = default!; // Nom du produit
    public string NameEn { get; set; } = default!; // Nom du produit
    public string CoverImage { get; set; } = default!; // Image de couverture
    public ProductType ProductType { get; set; } // Type de produit (Clothing, Accessory)
    public string ForOccasion { get; set; } = default!; // Occasion (e.g., casual, formal)
    public string Description { get; set; } = default!; // Description détaillée
    public string Material { get; set; } = default!; // Matériau (e.g., coton, cuir, métal)
    public bool IsHandmade { get; set; }  // Indique si le produit est fait main
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;// Date de création
    public DateTime UpdatedAt { get; set; }  // Date de mise à jour
    public string Collection { get; set; } = default!; // Collection associée
    public List<string> Categories { get; set; } = new(); // Liste des catégories
    public List<ColorVariant> ColorVariants { get; set; } = new();  // Redéfinit les variantes pour avoir des prix directs

};
public record CreateProductResult(Guid Id);
public class CreateProductCommandValidation : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidation()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.NameEn).NotEmpty().WithMessage("NameEn is required.");
        RuleFor(x => x.NameEn)
            .Matches(@"^[a-zA-Z0-9 \-]*$")
            .WithMessage("The field must not contain special characters.");
        RuleFor(x => x.ProductType).IsInEnum().WithMessage("The ProductType is required.");
        RuleFor(x => x.ProductType == ProductType.Clothing || x.ProductType == ProductType.Accessory).NotEmpty().WithMessage("The ProductType can only have value betwen accessory and clothing.");
        RuleFor(x => x.Categories).NotEmpty().WithMessage("The Category is required.");
        RuleFor(x => x.CoverImage).NotEmpty().WithMessage("The CoverImage is required.");
        RuleFor(x => x.ColorVariants.Count()).GreaterThan(0).WithMessage("ColorVariants is required.");
        RuleForEach(x => x.ColorVariants).ChildRules(color => color.RuleFor(x => x.Color).NotEmpty().WithMessage("The Color name is required."));
        RuleForEach(x => x.ColorVariants).ChildRules(color => color.RuleFor(x => x.Images.Count()).GreaterThan(0).WithMessage("The number of Images must be greater than 0."));
        When(x => x.ProductType == ProductType.Clothing, () =>
        {
            RuleForEach(x => x.ColorVariants).ChildRules(color => color.RuleFor(x => x.Sizes).NotNull().WithMessage("The Sizes can not be null."));
            RuleForEach(x => x.ColorVariants).ChildRules(color => color.RuleForEach(x => x.Sizes).ChildRules(size => size.RuleFor(x => x.Size).NotEmpty().WithMessage("Size is required for clothing products.")));
            RuleForEach(x => x.ColorVariants).ChildRules(color => color.RuleForEach(x => x.Sizes).ChildRules(size => size.RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.")));
        });
        When(x => x.ProductType == ProductType.Accessory, () =>
        {
            RuleForEach(x => x.ColorVariants).ChildRules(color => color.RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero."));
        });
    }
}
public class CreateProductCommandHandler(IDocumentSession session, IPublishEndpoint publishEndpoint)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>()
            .Where(_ => _.NameEn == command.NameEn
            || _.Name == command.Name).ToListAsync();
        if (products.Any())
        {
            throw new ProductAlreadyExistsFoundException("A product with the same name already exists.");
        }

        var product = CreateProduct(command);

        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        var eventMessage = product.Adapt<ProductCreatedEvent>();
        eventMessage.ProductType = product.ProductType.ToString();
        await publishEndpoint.Publish(eventMessage, cancellationToken);

        return new CreateProductResult(product.Id);
    }

    private Product CreateProduct(CreateProductCommand product)
    {
        var productType = product.ProductType;
        return new Product
        {
            Name = product.Name,
            NameEn = product.NameEn,
            CoverImage = product.CoverImage,
            ProductType = productType,
            ForOccasion = product.ForOccasion,
            Description = product.Description,
            Material = product.Material,
            IsHandmade = product.IsHandmade,
            Collection = product.Collection,
            Categories = product.Categories,
            CreatedAt = DateTime.Now,
            Colors = product.ColorVariants.Select(cv => cv.Color.ToLower()).ToList(),
            ColorVariants = product.ColorVariants.Select(cv => new ColorVariant
            {
                Color = cv.Color,
                Images = cv.Images,
                Slug = SlugHelper.GenerateSlug(product.NameEn, cv.Color),
                Price = productType == ProductType.Accessory ? cv.Price : null,
                Quantity = productType == ProductType.Accessory ? cv.Quantity : null,
                Sizes = productType == ProductType.Clothing ?
                cv.Sizes?.Select(s => new SizeVariant
                {
                    Size = s.Size,
                    Price = s.Price,
                    Quantity = s.Quantity
                }).ToList()
                : null
            }).ToList()
        };
    }
}