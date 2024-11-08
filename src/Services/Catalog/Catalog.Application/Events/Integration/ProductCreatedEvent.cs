namespace Catalog.Application.Events.Integration;

public record ProductCreatedEventIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; set; } 
    public string ProductType { get; set; } = default!;
    public List<ColorItem> ColorVariants { get; set; } = new();
}

public class ColorItem
{
    public int? Quantity { get; set; }  
    public string Color { get; set; } = default!;
    public List<SizeItem>? Sizes { get; set; } = new(); 
}
public class SizeItem
{
    public string Size { get; set; } = default!; 
    public int Quantity { get; set; } 
}