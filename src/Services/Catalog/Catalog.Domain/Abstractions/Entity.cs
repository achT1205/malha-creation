﻿namespace Catalog.Domain.Abstractions;

public abstract class Entity<T> : IEntity<T>
{
    protected Entity()
    {

    }
    public T Id { get; set; } = default!;
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}