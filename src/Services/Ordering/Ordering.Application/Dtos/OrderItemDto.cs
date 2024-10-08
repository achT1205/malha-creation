namespace Ordering.Application.Dtos;

public record OrderItemDto(Guid ProductId, int Quantity, string Color, string Size);