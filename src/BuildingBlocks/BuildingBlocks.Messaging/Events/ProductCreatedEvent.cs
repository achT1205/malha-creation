﻿namespace BuildingBlocks.Messaging.Events;

public record ProductCreatedEvent : IntegrationEvent
{
    public Guid Id { get; set; }  // Identifiant unique du produit
    public string Name { get; set; } = default!; // Nom du produit
    public string ProductType { get; set; } = default!;// Type de produit (Clothing, Accessory)
    public List<ColorItem> ColorVariants { get; set; } = new();
}

public record ProductUpdatedEvent : IntegrationEvent
{
    public Guid Id { get; set; }  // Identifiant unique du produit
    public string Name { get; set; } = default!; // Nom du produit
    public string ProductType { get; set; } = default!;// Type de produit (Clothing, Accessory)
    public List<ColorItem> ColorVariants { get; set; } = new();
}


public record ProductDeletedEvent : IntegrationEvent
{
    public Guid Id { get; set; }  // Identifiant unique du produit
}

public class ColorItem
{
    public int? Quantity { get; set; }  // Quantité disponible pour cette couleur
    public string Color { get; set; } = default!; // Nom ou identifiant de la couleur
    public string Slug { get; set; } = default!; // Identifiant "slug" pour l'URL
    public List<SizeItem>? Sizes { get; set; } = new(); // Liste des tailles disponibles pour cette couleur
}
public class SizeItem
{
    public string Size { get; set; } = default!; // Taille (e.g., S, M, L)
    public int Quantity { get; set; }  // Quantité disponible pour cette taille
}