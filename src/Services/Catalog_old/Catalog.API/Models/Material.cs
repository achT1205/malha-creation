﻿namespace Catalog.API.Models;
public class Material
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}