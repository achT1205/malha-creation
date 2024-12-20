﻿namespace Catalog.Application.Interfaces;
public interface IProductRepository
{
    Task<Product?> GetProductWithDetailsAsync(ProductId productId);
    Task<List<Product>> GetProducstWithDetailsAsync();
    Task<Product> GetByIdAsync(ProductId id);
    Task<Product> GetByIdWithColorVariantAsync(ProductId id);
    Task<Product?> GetBySlugAsync(string slug);
    Task<(List<Product>, long)> GetAllAsync();
    Task AddAsync(Product product);
    void UpdateAsync(Product product);
    void RemoveAsync(Product product);
    Task SaveChangesAsync();
}

