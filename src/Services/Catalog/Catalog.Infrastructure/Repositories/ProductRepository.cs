﻿using Catalog.Application.Interfaces;
using Catalog.Infrastructure.Data;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Product> GetByIdAsync(ProductId id)
    {
        return await _context.Products
            .Include(p => p.ColorVariants)
                .ThenInclude(cv => cv.Images)
            .Include(p => p.ColorVariants)
                .ThenInclude(cv => (cv).SizeVariants)
            .FirstOrDefaultAsync(product => product.Id == id)
            ?? throw new KeyNotFoundException($"Product with ID {id.Value} not found.");
    }

    public async Task<Product> GetByIdWithColorVariantAsync(ProductId id)
    {
        return await _context.Products
            .Include(p => p.ColorVariants)
            .FirstOrDefaultAsync(product => product.Id == id)
            ?? throw new KeyNotFoundException($"Product with ID {id.Value} not found.");
    }

    public async Task<Product?> GetBySlugAsync(string slug)
    {
        return await _context.Products
            .Include(p => p.ColorVariants)
                .ThenInclude(cv => cv.Images)
            .Include(p => p.ColorVariants)
                .ThenInclude(cv => (cv).SizeVariants)
            .FirstOrDefaultAsync(p => p.ColorVariants.Any(cv => cv.Slug.Value == slug));
    }

    // Récupérer tous les produits
    public async Task<(List<Product>, long)> GetAllAsync()
    {
        var totalCount = await _context.Products.LongCountAsync();

        var products = await _context.Products
            .Include(p => p.ColorVariants)
                .ThenInclude(cv => cv.Images)
            .Include(p => p.ColorVariants)
                .ThenInclude(cv => (cv).SizeVariants)
            .ToListAsync();

        return (products, totalCount);
    }

    public async Task<List<Product>> GetProducstWithDetailsAsync()
    {
        return await _context.Products
            .Include(p => p.ColorVariants)
            .Include(p => p.Reviews)
            .Include(p => p.CategoryIds)
            .Include(p => p.OccasionIds)
            .ToListAsync();
    }

    public async Task<Product?> GetProductWithDetailsAsync(ProductId productId)
    {
        return await _context.Products
            .Include(p => p.ColorVariants)
            .Include(p => p.Reviews)
            .Include(p => p.CategoryIds)
            .Include(p => p.OccasionIds)
            .FirstOrDefaultAsync(p => p.Id == productId);
    }

    public async Task AddAsync(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }
        await _context.Products.AddAsync(product);
    }

    // Mettre à jour un produit existant
    public void UpdateAsync(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }
        _context.Entry(product).Property("Name_Value").CurrentValue = product.Name.Value;
        _context.Entry(product).Property("UrlFriendlyName_Value").CurrentValue = product.UrlFriendlyName.Value;
        _context.Products.Update(product);
    }

    // Supprimer un produit
    public void RemoveAsync(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }
        _context.Products.Remove(product);
    }

    // Sauvegarder les changements dans la base de données
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
