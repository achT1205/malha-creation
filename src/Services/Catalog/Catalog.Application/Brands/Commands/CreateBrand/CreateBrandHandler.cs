namespace Catalog.Application.Brands.Commands.CreateBrand;


public record CreateBrandCommand(string Name)
    : ICommand<CreateBrandResult>;
public record CreateBrandResult(Guid Id);


public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
{
    public CreateBrandCommandValidator()
    {
        RuleFor(x => x.Name).NotNull().WithMessage("Brand name is required");
    }
}
public class CreateBrandCommandHandler : ICommandHandler<CreateBrandCommand, CreateBrandResult>
{
    private readonly IBrandRepository _brandRepository;
    public CreateBrandCommandHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }
    public async Task<CreateBrandResult> Handle(CreateBrandCommand command, CancellationToken cancellationToken)
    {
        var brand = Brand.Create(BrandName.Of(command.Name));

        await _brandRepository.AddAsync(brand);
        await _brandRepository.SaveChangesAsync();

        return new CreateBrandResult(brand.Id.Value);
    }
}
