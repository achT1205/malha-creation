using BuildingBlocks.CQRS;
using Catalog.Application.Interfaces;
using Catalog.Domain.ValueObjects;
using FluentValidation;

namespace Catalog.Application.Occasions.Commands.CreateOccasion;


public record CreateOccasionCommand(
    string Name
    )
    : ICommand<CreateOccasionResult>;
public record CreateOccasionResult(Guid Id);


public class CreateOccasionCommandValidator : AbstractValidator<CreateOccasionCommand>
{
    public CreateOccasionCommandValidator()
    {
        RuleFor(x => x.Name).NotNull().WithMessage("Occasion name is required");
    }
}
public class CreateOccasionCommandHandler : ICommandHandler<CreateOccasionCommand, CreateOccasionResult>
{
    private readonly IOccasionRepository _OccasionRepository;
    public CreateOccasionCommandHandler(IOccasionRepository OccasionRepository)
    {
        _OccasionRepository = OccasionRepository;
    }
    public async Task<CreateOccasionResult> Handle(CreateOccasionCommand command, CancellationToken cancellationToken)
    {
        var occasion = Occasion.Create(OccasionName.Of(command.Name));
        await _OccasionRepository.AddAsync(occasion);
        await _OccasionRepository.SaveChangesAsync();
        return new CreateOccasionResult(occasion.Id.Value);
    }
}
