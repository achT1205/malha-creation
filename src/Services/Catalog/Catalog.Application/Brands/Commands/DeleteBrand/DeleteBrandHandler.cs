namespace Catalog.Application.Brands.Commands.DeleteBrand;

public record DeleteBrandCommand(Guid Id) : ICommand<DeleteBrandResult>;
public record DeleteBrandResult(bool IsSuccess);

public class DeleteBrandCommandValidation : AbstractValidator<DeleteBrandCommand>
{
    public DeleteBrandCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Brand Id is required");
    }
}
public class DeleteProducCommandtHandler : ICommandHandler<DeleteBrandCommand, DeleteBrandResult>
{
    private readonly IBrandRepository  _brandRepository ;
    public DeleteProducCommandtHandler(IBrandRepository brandRepository )
    {
        _brandRepository = brandRepository;

    }

    public async Task<DeleteBrandResult> Handle(DeleteBrandCommand command, CancellationToken cancellationToken)
    {
        var b = await _brandRepository.GetByIdAsync(BrandId.Of(command.Id));
        if (b != null)
        {
           await _brandRepository.RemoveAsync(b);
            await _brandRepository.SaveChangesAsync();
        }
        return new DeleteBrandResult(true);
    }
}
