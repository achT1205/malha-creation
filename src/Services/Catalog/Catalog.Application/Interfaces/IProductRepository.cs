﻿using Catalog.Domain.ValueObjects;

namespace Catalog.Application.Interfaces;
public interface IProductRepository
{
    //Task<Product> GetByIdAsync(ProductId id);
    //Task<Product?> GetBySlugAsync(string slug);
    //Task<List<Product>> GetAllAsync();
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task RemoveAsync(Product product);
    Task SaveChangesAsync();
}
