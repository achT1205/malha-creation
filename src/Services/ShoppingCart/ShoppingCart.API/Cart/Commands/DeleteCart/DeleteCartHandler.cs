namespace Cart.API.Cart.Commands.DeleteCart;

public record DeleteCartCommand(Guid UserId) : ICommand<DeleteCartResult>;
public record DeleteCartResult(bool IsSuccess);

public class DeleteCartCommandValidator : AbstractValidator<DeleteCartCommand>
{
    public DeleteCartCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
    }
}

internal class DeleteCartCommandHandler(ICartRepository repository) : ICommandHandler<DeleteCartCommand, DeleteCartResult>
{
    public async Task<DeleteCartResult> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
    {
        await repository.DeleteCart(request.UserId, cancellationToken);

        return new DeleteCartResult(true);
    }
}