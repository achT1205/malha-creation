using BuildingBlocks.Exceptions;
using Cart.API.Services.Interfaces;
using Discount.Grpc;
using ShoppingCart.API.Enums;

namespace Cart.API.Cart.Commands.StoreCart;

public record StoreCartCommand : ICommand<StoreCartResult>
{
    public Guid UserId { get; set; } = default!;
    public List<CartItem> Items { get; set; } = new();
};
public record StoreCartResult(Basket Basket);

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
            var cart = new Basket { UserId = command.UserId, Items = items.ToList() };

            await repository.StoreCart(cart, cancellationToken);

            return new StoreCartResult(cart);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private async Task<IEnumerable<BasketItem>> DeductDiscount(IEnumerable<CartItem> cartItems, CancellationToken cancellationToken)
    {
        List<BasketItem> BasketItems = new List<BasketItem>();

        foreach (var item in cartItems)
        {
            var product = await productService.GetProductByIdAsync(item.ProductId);
            if (product == null)
            {
                throw new ProductNotFoundException(item.ProductId);
            }

            var BasketItem = new BasketItem
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
            BasketItem.Slug = variant.Slug;
            if (product.ProductType == ProductType.Clothing.ToString())
            {
                var size = variant?.SizeVariants?.FirstOrDefault(x => x.Size.ToLower() == item.Size.ToLower());
                if (size == null)
                {
                    throw new ProductNotFoundException($"No size {item.Size} exists for this product in the color {item.Color} ");
                }
                BasketItem.Price = size.Price;
            }
            else
            {
                BasketItem.Price = variant.Price.Amount;
            }
            var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductId = item.ProductId.ToString() }, cancellationToken: cancellationToken);
            BasketItem.Price -= coupon.Amount;

            BasketItems.Add(BasketItem);
        }

        return BasketItems;
    }
}