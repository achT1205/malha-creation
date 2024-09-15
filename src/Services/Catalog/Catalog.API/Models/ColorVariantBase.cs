namespace Catalog.API.Models;
// Classe de base pour les variantes de couleur
public abstract class ColorVariantBase
{
    public string Color { get; set; } = default!; // Nom ou identifiant de la couleur
    public string CoverImage { get; set; } = default!; // URL de l'image de couverture pour cette couleur
    public List<string> Images { get; set; } = new(); // Liste des images supplémentaires
    public string Slug { get; set; } = default!; // Identifiant "slug" pour l'URL
}