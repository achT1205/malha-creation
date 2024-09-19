using BuildingBlocks.Messaging.Events;
using Catalog.API.Helpers;

namespace Catalog.API.Products.Commands.UpdateProduct;
public record UpdateProductCommand(Product Product)
    : ICommand<UpdateProductResult>;
public record UpdateProductResult(bool IsSuccess);
public class UpdateProductCommandValidation : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidation()
    {
        RuleFor(x => x.Product.Id).NotEmpty().WithMessage("Product ID is required");
        RuleFor(x => x.Product.NameEn).NotEmpty().WithMessage("NameEn is required.");
        RuleFor(x => x.Product.NameEn)
            .Matches(@"^[a-zA-Z0-9]*$")
            .WithMessage("The field must not contain special characters.");
        RuleFor(x => x.Product.ProductType).NotEmpty().WithMessage("The ProductType is required.");
        RuleFor(x => x.Product.ProductType.ToLower() == "clothing" || x.Product.ProductType.ToLower() == "accessory").NotEmpty().WithMessage("The ProductType can only have value betwen accessory and clothing.");
        RuleFor(x => x.Product.Categories).NotEmpty().WithMessage("The Category is required.");
        RuleFor(x => x.Product.CoverImage).NotEmpty().WithMessage("The CoverImage is required.");
        RuleFor(x => x.Product.ColorVariants.Count()).GreaterThan(0).WithMessage("ColorVariants is required.");
        RuleForEach(x => x.Product.ColorVariants).ChildRules(color => color.RuleFor(x => x.Color).NotEmpty().WithMessage("The Color name is required."));
        RuleForEach(x => x.Product.ColorVariants).ChildRules(color => color.RuleFor(x => x.CoverImage).NotEmpty().WithMessage("The CoverImage is required."));
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
public class UpdateProductCommandHandler(IDocumentSession session, IPublishEndpoint publishEndpoint)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {

        var product = await session.LoadAsync<Product>(command.Product.Id, cancellationToken);
        if (product == null)
        {
            throw new ProductNotFoundException(command.Product.Id);
        }

        product.Name = command.Product.Name;
        product.NameEn = command.Product.NameEn;
        product.CoverImage = command.Product.CoverImage;
        product.ProductType = command.Product.ProductType;
        product.ForOccasion = command.Product.ForOccasion;
        product.Description = command.Product.Description;
        product.Material = command.Product.Material;
        product.IsHandmade = command.Product.IsHandmade;
        product.Collection = command.Product.Collection;
        product.Categories = command.Product.Categories;
        product.ColorVariants = command.Product.ColorVariants.Select(cv => new ColorVariant
        {
            Color = cv.Color,
            CoverImage = cv.CoverImage,
            Images = cv.Images,
            Slug = SlugHelper.GenerateSlug(command.Product.NameEn, cv.Color),
            Price = command.Product.ProductType == "accessory" ? cv.Price : null,
            Quantity = command.Product.ProductType == "accessory" ? cv.Quantity : null,
            Sizes = product.ProductType == "clothing" ?
            cv.Sizes?.Select(s => new SizeVariant
            {
                Size = s.Size,
                Price = s.Price,
                Quantity = s.Quantity
            }).ToList() : null
        }).ToList();
        var eventMessage = product.Adapt<ProductCreatedEvent>();
        await publishEndpoint.Publish(eventMessage, cancellationToken);

        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }
}
