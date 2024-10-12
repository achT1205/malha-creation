﻿using Ordering.Application.Abstractions.Models;

namespace Ordering.Application.Abstractions.Services;

public interface IProductService
{
    Task<Product?> GetProductByIdAsync(Guid productId);
}
