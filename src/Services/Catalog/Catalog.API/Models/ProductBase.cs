﻿namespace Catalog.API.Models;
public class ProductBase
{
    public Guid Id { get; set; }  // Identifiant unique du produit
    public string Name { get; set; } = default!; // Nom du produit
    public string NameEn { get; set; } = default!; // Nom du produit
    public string CoverImage { get; set; } = default!; // Image de couverture
    public string ProductType { get; set; } = default!;// Type de produit (Clothing, Accessory)
    public string ForOccasion { get; set; } = default!; // Occasion (e.g., casual, formal)
    public string Description { get; set; } = default!; // Description détaillée
    public string Material { get; set; } = default!; // Matériau (e.g., coton, cuir, métal)
    public bool IsHandmade { get; set; }  // Indique si le produit est fait main
    public DateTime CreatedAt { get; set; }  // Date de création
    public DateTime UpdatedAt { get; set; }  // Date de mise à jour
    public string Collection { get; set; } = default!; // Collection associée
    public List<string> Categories { get; set; } = new(); // Liste des catégories
}