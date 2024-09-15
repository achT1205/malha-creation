
namespace Catalog.API.Materials.Commands.DeleteMaterial;
public record DeleteMaterialCommand(Guid Id):ICommand<DeleteMaterialResult>;
public record DeleteMaterialResult(bool IsSuccess);

public class DeleteMaterialCommandValidator : AbstractValidator<DeleteMaterialCommand>
{
    public DeleteMaterialCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Material ID is required");
    }
}
public class DeleteMaterialCommandHandler(IDocumentSession session) : ICommandHandler<DeleteMaterialCommand, DeleteMaterialResult>
{
    public async Task<DeleteMaterialResult> Handle(DeleteMaterialCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Material>(command.Id, cancellationToken);
        if (product == null)
        {
            throw new MaterialNotFoundException(command.Id);
        }
        session.Delete<Material>(command.Id);
        await session.SaveChangesAsync(cancellationToken);
        return new DeleteMaterialResult(true);
    }
}