namespace Cart.API.Dtos;
public class ApplyCartDiscountDto
{
    public Guid UserId { get; set; }
    public string CouponCode { get; set; } = default!;
}