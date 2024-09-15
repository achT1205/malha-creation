namespace Catalog.API.Products.Commands.CreateProduct;

public record CreateClothingProductCommand(Product Product)
    : ICommand<CreateClothingProductResult>;
public record CreateClothingProductResult(Guid Id);

public class CreateClothingProductCommandValidation : AbstractValidator<CreateClothingProductCommand>
{
    public CreateClothingProductCommandValidation()
    {
        RuleFor(x => x.Product.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Product.ProductType).NotEmpty().WithMessage("ProductType is required");
        RuleFor(x => x.Product.Categories).NotEmpty().WithMessage("Category is required");
        RuleFor(x => x.Product.CoverImage).NotEmpty().WithMessage("CoverImage is required");
        RuleFor(x => x.Product.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}
public class CreateClothingProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateClothingProductCommand, CreateClothingProductResult>
{
    public async Task<CreateClothingProductResult> Handle(CreateClothingProductCommand command, CancellationToken cancellationToken)
    {
        var Product = new Product
        {
            Name = command.Product.Name,
            CoverImage = command.Product.CoverImage,
            ProductType = command.Product.ProductType,
            ForOccasion = command.Product.ForOccasion,
            Description = command.Product.Description,
            Material = command.Product.Material,
            IsHandmade = command.Product.IsHandmade,
            CreatedAt = DateTime.Now,
            Collection = command.Product.Collection,
            Categories = command.Product.Categories,
            Price = command.Product.Price,
            Colors = command.Product.Colors,
            Sizes = command.Product.Sizes,
        };

        session.Store(Product);
        await session.SaveChangesAsync(cancellationToken);
        return new CreateClothingProductResult(Product.Id);
    }
}