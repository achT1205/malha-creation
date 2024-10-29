using BuildingBlocks.Enums;
using BuildingBlocks.Exceptions;
using Cart.API.Services.Interfaces;
using Discount.Grpc;

namespace Cart.API.Cart.Commands.StoreCart;

public record StoreCartCommand : ICommand<StoreCartResult>
{
    public Guid UserId { get; set; } = default!;
    public List<CartItem> Items { get; set; } = new();
};
public record StoreCartResult(ShoppingCart ShoppingCart);

public class StoreCartCommandValidator : AbstractValidator<StoreCartCommand>
{
    public StoreCartCommandValidator()
    {
        RuleFor(x => x.UserId).NotNull().WithMessage("StoreCartHandler is required");
        RuleFor(x => x.Items).NotNull().WithMessage("StoreCartHandler is required");
    }
}
public class StoreCartCommandHandler(
    ICartRepository repository,
    DiscountProtoService.DiscountProtoServiceClient discountProto,
    IProductService productService)
    : ICommandHandler<StoreCartCommand, StoreCartResult>
{
    public async Task<StoreCartResult> Handle(StoreCartCommand command, CancellationToken cancellationToken)
    {
        try
        {

            var items = await DeductDiscount(command.Items, cancellationToken);
            var cart = new ShoppingCart { UserId = command.UserId, Items = items.ToList() };

            await repository.StoreCart(cart, cancellationToken);

            return new StoreCartResult(cart);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private async Task<IEnumerable<ShoppingCartItem>> DeductDiscount(IEnumerable<CartItem> cartItems, CancellationToken cancellationToken)
    {
        List<ShoppingCartItem> shoppingCartItems = new List<ShoppingCartItem>(); ;

        foreach (var item in cartItems)
        {
            var product = await productService.GetProductByIdAsync(item.ProductId);
            if (product == null)
            {
                throw new ProductNotFoundException(item.ProductId);
            }

            var shoppingCartItem = new ShoppingCartItem
            {
                Quantity = item.Quantity,
                Color = item.Color,
                Size = item.Size,
                ProductId = item.ProductId,
                ProductName = product.Name

            };

            var variant = product.ColorVariants.FirstOrDefault(x => x.Color.ToLower() == item.Color.ToLower());
            if (variant == null)
            {
                throw new ProductNotFoundException($"No variant exists for this product in the color {item.Color}");
            }
            shoppingCartItem.Slug = variant.Slug;
            if (product.ProductType == ProductTypeEnum.Clothing.ToString())
            {
                var size = variant?.SizeVariants?.FirstOrDefault(x => x.Size.ToLower() == item.Size.ToLower());
                if (size == null)
                {
                    throw new ProductNotFoundException($"No size {item.Size} exists for this product in the color {item.Color} ");
                }
                shoppingCartItem.Price = size.Price;
            }
            else
            {
                shoppingCartItem.Price = variant.Price.Amount;
            }
            var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductId = item.ProductId.ToString() }, cancellationToken: cancellationToken);
            shoppingCartItem.Price -= coupon.Amount;

            shoppingCartItems.Add(shoppingCartItem);
        }

        return shoppingCartItems;
    }
}