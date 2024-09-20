using BuildingBlocks.Messaging.Events;
using Catalog.API.Helpers;

namespace Catalog.API.Products.Commands.CreateProduct;
public record CreateProductCommand(Product Product)
    : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);
public class CreateProductCommandValidation : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidation()
    {
        RuleFor(x => x.Product.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Product.NameEn).NotEmpty().WithMessage("NameEn is required.");
        RuleFor(x => x.Product.NameEn)
            .Matches(@"^[a-zA-Z0-9 \-]*$")
            .WithMessage("The field must not contain special characters.");
        RuleFor(x => x.Product.ProductType).NotEmpty().WithMessage("The ProductType is required.");
        RuleFor(x => x.Product.ProductType.ToLower() == "clothing" || x.Product.ProductType.ToLower() == "accessory").NotEmpty().WithMessage("The ProductType can only have value betwen accessory and clothing.");
        RuleFor(x => x.Product.Categories).NotEmpty().WithMessage("The Category is required.");
        RuleFor(x => x.Product.CoverImage).NotEmpty().WithMessage("The CoverImage is required.");
        RuleFor(x => x.Product.ColorVariants.Count()).GreaterThan(0).WithMessage("ColorVariants is required.");
        RuleForEach(x => x.Product.ColorVariants).ChildRules(color => color.RuleFor(x => x.Color).NotEmpty().WithMessage("The Color name is required."));
        RuleForEach(x => x.Product.ColorVariants).ChildRules(color => color.RuleFor(x => x.Images.Count()).GreaterThan(0).WithMessage("The number of Images must be greater than 0."));
        When(x => x.Product.ProductType.ToLower() == "clothing", () =>
        {
            RuleForEach(x => x.Product.ColorVariants).ChildRules(color => color.RuleFor(x => x.Sizes).NotNull().WithMessage("The Sizes can not be null."));
            RuleForEach(x => x.Product.ColorVariants).ChildRules(color => color.RuleForEach(x => x.Sizes).ChildRules(size => size.RuleFor(x => x.Size).NotEmpty().WithMessage("Size is required for clothing products.")));
            RuleForEach(x => x.Product.ColorVariants).ChildRules(color => color.RuleForEach(x => x.Sizes).ChildRules(size => size.RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.")));
        });
        When(x => x.Product.ProductType.ToLower() == "accessory", () =>
        {
            RuleForEach(x => x.Product.ColorVariants).ChildRules(color => color.RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero."));
        });
    }
}
public class CreateProductCommandHandler(IDocumentSession session, IPublishEndpoint publishEndpoint)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>()
            .Where(_ => _.NameEn == command.Product.NameEn
            || _.Name == command.Product.Name).ToListAsync();
        if (products.Any())
        {
            throw new ProductAlreadyExistsFoundException("A product with the same name already exists.");
        }

        var product = CreateProduct(command.Product);
        var eventMessage = product.Adapt<ProductCreatedEvent>();
        await publishEndpoint.Publish(eventMessage, cancellationToken);

        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }

    private Product CreateProduct(Product product)
    {
        var productType = product.ProductType.ToLower();
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
                Price = productType == "accessory" ? cv.Price : null,
                Quantity = productType == "accessory" ? cv.Quantity : null,
                Sizes = productType == "clothing" ?
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