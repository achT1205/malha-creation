using Catalog.Domain.Enums;
using FluentValidation;

namespace Catalog.Application.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.UrlFriendlyName).NotEmpty().WithMessage("UrlFriendlyName is required.");
        RuleFor(x => x.ProductType).IsInEnum().WithMessage("The ProductType is required.");
        RuleFor(x => x.ProductType == ProductTypeEnum.Clothing || x.ProductType == ProductTypeEnum.Accessory).NotEmpty().WithMessage("The ProductType can only have value betwen accessory and clothing.");
        RuleFor(x => x.UrlFriendlyName)
            .Matches(@"^[a-zA-Z0-9 \-]*$")
            .WithMessage("The field must not contain special characters.");
        RuleFor(x => x.ProductTypeId).IsInEnum().WithMessage("The ProductTypeId is required.");
        RuleFor(x => x.CategoryIds).NotEmpty().WithMessage("The CategoryIds are required.");
        RuleFor(x => x.CoverImage).NotEmpty().WithMessage("The CoverImage is required.");
        RuleFor(x => x.ColorVariants.Count()).GreaterThan(0).WithMessage("ColorVariants is required.");
        RuleForEach(x => x.ColorVariants).ChildRules(color => color.RuleFor(x => x.Color).NotEmpty().WithMessage("The Color name is required."));
        RuleForEach(x => x.ColorVariants).ChildRules(color => color.RuleFor(x => x.Images.Count()).GreaterThan(0).WithMessage("The number of Images must be greater than 0."));
        When(x => x.ProductType == ProductTypeEnum.Clothing, () =>
        {
            RuleForEach(x => x.ColorVariants).ChildRules(color => color.RuleFor(x => x.sizeVariants).NotNull().WithMessage("The Sizes can not be null."));
            RuleForEach(x => x.ColorVariants).ChildRules(color => color.RuleForEach(x => x.sizeVariants).ChildRules(size => size.RuleFor(x => x.Size).NotEmpty().WithMessage("Size is required for clothing products.")));
            RuleForEach(x => x.ColorVariants).ChildRules(color => color.RuleForEach(x => x.sizeVariants).ChildRules(size => size.RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.")));
        });
        When(x => x.ProductType == ProductTypeEnum.Accessory, () =>
        {
            RuleForEach(x => x.ColorVariants).ChildRules(color => color.RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero."));
        });
    }
}
