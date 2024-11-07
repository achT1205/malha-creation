namespace Catalog.Application.Dtos;

public record SizeVariantDto
    (
    string Size,
    decimal Price,
    string Currency,
    int Quantity,
    int RestockThreshold
    );
