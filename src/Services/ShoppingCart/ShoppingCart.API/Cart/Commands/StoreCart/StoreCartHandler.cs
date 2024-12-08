using BuildingBlocks.Exceptions;
using Cart.API.Services.Interfaces;
using Discount.Grpc;
using ShoppingCart.API.Dtos;
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
        RuleFor(x => x.UserId).NotNull().WithMessage("Cart holder is required.");
        RuleFor(x => x.Items).NotNull().WithMessage("Cart Items can not be null.");
        RuleForEach(x => x.Items).ChildRules(item => item.RuleFor(x => x.ProductId).NotNull().WithMessage("The ProductId is required."));
        RuleForEach(x => x.Items).ChildRules(item => item.RuleFor(x => x.ColorVariantId).NotNull().WithMessage("The ColorVariantId is required."));
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
        List<BasketItem> basketItems = new List<BasketItem>();

        foreach (var item in cartItems)
        {
            var product = await productService.GetProductByIdAsync(item.ProductId);
            if (product == null)
            {
                throw new ProductNotFoundException(item.ProductId);
            }


            var basketItem = new BasketItem
            {
                Quantity = item.Quantity,
                ProductId = item.ProductId,
                ColorVariantId = item.ColorVariantId,
                SizeVariantId = item.SizeVariantId,
                ProductName = product.Name
            };

            var cv = product.ColorVariants.Find(x => x.Id == item.ColorVariantId);
            if (cv == null)
            {
                throw new ProductNotFoundException($"No variant exists for this product in the colorId {item.ColorVariantId}");
            }
            basketItem.Color = cv.Color;
            basketItem.Slug = cv.Slug;
            basketItem.CoverImage = cv.Images[0].ImageSrc;
            if (product.ProductType == ProductType.Clothing)
            {
                var sv = cv.SizeVariants.Find(x => x.Id == item.SizeVariantId);
                if (sv == null)
                {
                    throw new ProductNotFoundException($"No sizeId {item.SizeVariantId} exists for this product in the color {cv.Color} ");
                }
                if(sv.Quantity < basketItem.Quantity)
                {
                    throw new ProductNotFoundException($"The size variant {item.SizeVariantId} is out of stock.");
                }
                basketItem.Size = sv.Size;
                basketItem.Price = sv.Price;
            }
            else
            {
                if (cv.Quantity < basketItem.Quantity)
                {
                    throw new ProductOutOfStockFoundException($"The color variant {item.ColorVariantId} is out of stock.");
                }
                basketItem.Price = cv.Price.Amount.Value;
            }
            var coupon = await discountProto.GetCouponForProductAsync(new GetCouponForProductRequest { ProductId = item.ProductId.ToString(), ProductPrice = (double)basketItem.Price }, cancellationToken: cancellationToken);
            basketItem.Price = (decimal)coupon.DiscountedPrice;
            basketItem.Coupon = coupon.Adapt<CouponModel>();

            basketItems.Add(basketItem);
        }

        return basketItems;
    }
}