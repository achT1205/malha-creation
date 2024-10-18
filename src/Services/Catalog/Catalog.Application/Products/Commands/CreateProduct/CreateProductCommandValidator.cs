using FluentValidation;

namespace Catalog.Application.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        //RuleFor(x => x.CoverImageId).NotNull().WithMessage("CoverImageId is required");
        //RuleFor(x => x.Name).NotNull().WithMessage("Name is required");
        //RuleFor(x => x.UrlFriendlyName).NotNull().WithMessage("UrlFriendlyName is required");
        //RuleFor(x => x.ProductTypeId).NotNull().WithMessage("ProductTypeId is required");
        //RuleFor(x => x.Description).NotNull().WithMessage("Description is required");
    }
}
