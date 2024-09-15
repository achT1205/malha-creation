namespace Catalog.API.Materials.Commands.CreateMaterial;

public record CreateMaterialCommand(Material material)
    : ICommand<CreateMaterialResult>;
public record CreateMaterialResult(Guid Id);

public class CreateMaterialCommandValidation : AbstractValidator<CreateMaterialCommand>
{
    public CreateMaterialCommandValidation()
    {
        RuleFor(x => x.material.Name).NotEmpty().WithMessage("Name is required");
    }
}

public class CreateMaterialCommandHandler(IDocumentSession session) : ICommandHandler<CreateMaterialCommand, CreateMaterialResult>
{
    public async Task<CreateMaterialResult> Handle(CreateMaterialCommand command, CancellationToken cancellationToken)
    {
        var material = new Material
        {
            Name = command.material.Name,
            Description = command.material.Description
        };

        session.Store(material);
        await session.SaveChangesAsync(cancellationToken);
        return new CreateMaterialResult(material.Id);
    }
}