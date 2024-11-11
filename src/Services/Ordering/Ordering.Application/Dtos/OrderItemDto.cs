namespace Ordering.Application.Dtos;

public record OrderItemDto(Guid ProductId, string ProductName, int Quantity, string Color, string? Size, decimal Price, string Slug);