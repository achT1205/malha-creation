using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Models;

public class ProductType
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;
}
