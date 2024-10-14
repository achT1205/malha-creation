using Catalog.Domain.Abstractions;
using Catalog.Domain.ValueObjects;

namespace Catalog.Domain.Models;


public class Category : Entity<CategoryId>
{
    public string Name { get; private set; } = default!;

    Category(CategoryId id, string name)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}