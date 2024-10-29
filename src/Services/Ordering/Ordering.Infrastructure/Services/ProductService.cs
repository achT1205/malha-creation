﻿using BuildingBlocks.Exceptions;
using Microsoft.Extensions.Options;
using Ordering.Application.Abstractions.Models;
using Ordering.Application.Abstractions.Services;
using Ordering.Infrastructure.Configs;
using System.Text.Json;

namespace Ordering.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    private readonly string _productApiUrl;
    public ProductService(HttpClient httpClient, IOptions<ExternalApiSettings> externalApiSettings)
    {
        _httpClient = httpClient;
        _productApiUrl = externalApiSettings.Value.ProductApiUrl;
    }

    public async Task<Product?> GetProductByIdAsync(Guid productId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_productApiUrl}/{productId}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                var resp = JsonSerializer.Deserialize<ProductApiResponse>(jsonString, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                return resp?.Product;
            }

            throw new InternalServerException(response.Content.ToString());
        }
        catch (Exception ex)
        {

            throw new InternalServerException(ex.InnerException.Message);
        }
    }
}
