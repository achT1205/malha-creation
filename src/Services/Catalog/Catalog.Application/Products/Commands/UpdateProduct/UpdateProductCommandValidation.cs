using BuildingBlocks.Enums;
using FluentValidation;

namespace Catalog.Application.Products.Commands.UpdateProduct;


public class UpdateProductCommandValidation : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required");
        RuleFor(x => x.UrlFriendlyName).NotEmpty().WithMessage("UrlFriendlyName is required.");
        RuleFor(x => x.UrlFriendlyName)
            .Matches(@"^[a-zA-Z0-9 \-]*$")
            .WithMessage("The field must not contain special characters.");
        RuleFor(x => x.ProductType == ProductTypeEnum.Clothing || x.ProductType == ProductTypeEnum.Accessory).NotEmpty().WithMessage("The ProductType can only have value betwen accessory and clothing.");
        RuleFor(x => x.CategoryIds).NotEmpty().WithMessage("The Category is required.");
        RuleFor(x => x.CoverImage).NotEmpty().WithMessage("The CoverImage is required.");
        RuleFor(x => x.ColorVariants.Count()).GreaterThan(0).WithMessage("ColorVariants is required.");
        RuleForEach(x => x.ColorVariants).ChildRules(color => color.RuleFor(x => x.Color).NotEmpty().WithMessage("The Color name is required."));
        RuleForEach(x => x.ColorVariants).ChildRules(color => color.RuleFor(x => x.Images.Count()).GreaterThan(0).WithMessage("The number of Images must be greater than 0."));
        When(x => x.ProductType == ProductTypeEnum.Clothing, () =>
        {
            RuleForEach(x => x.ColorVariants).ChildRules(color => color.RuleFor(x => x.sizeVariants).NotEmpty().WithMessage("The Sizes can not be null."));
            RuleForEach(x => x.ColorVariants).ChildRules(color => color.RuleForEach(x => x.sizeVariants).ChildRules(size => size.RuleFor(x => x.Size).NotEmpty().WithMessage("Size is required for clothing products.")));
            RuleForEach(x => x.ColorVariants).ChildRules(color => color.RuleForEach(x => x.sizeVariants).ChildRules(size => size.RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.")));
        })
        .Otherwise(() =>
        {
            RuleForEach(x => x.ColorVariants).ChildRules(color => color.RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero."));
        });
    }
}