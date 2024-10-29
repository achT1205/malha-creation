using BuildingBlocks.CQRS;
using Catalog.Application.Interfaces;
using FluentValidation;

namespace Catalog.Application.ProductTypes.Commands.CreateProductType;


public record CreateProductTypeCommand(string Name) : ICommand<CreateProductTypeResult>;
public record CreateProductTypeResult(Guid Id);


public class CreateProductTypeCommandValidator : AbstractValidator<CreateProductTypeCommand>
{
    public CreateProductTypeCommandValidator()
    {
        RuleFor(x => x.Name).NotNull().WithMessage("ProductType name is required");
    }
}
public class CreateProductTypeCommandHandler : ICommandHandler<CreateProductTypeCommand, CreateProductTypeResult>
{
    private readonly IProductTypeRepository _productTypeRepository;
    public CreateProductTypeCommandHandler(IProductTypeRepository productTypeRepository)
    {
        _productTypeRepository = productTypeRepository;
    }
    public async Task<CreateProductTypeResult> Handle(CreateProductTypeCommand command, CancellationToken cancellationToken)
    {
        var type = ProductType.Create(command.Name);
        await _productTypeRepository.AddAsync(type);
        await _productTypeRepository.SaveChangesAsync();
        return new CreateProductTypeResult(type.Id.Value);
    }
}
