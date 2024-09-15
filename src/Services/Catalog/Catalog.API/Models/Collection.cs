namespace Catalog.API.Models;
public class Collection
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string CoverImage { get; set; } = default!;

}
