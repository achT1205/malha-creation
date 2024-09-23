using Inventory.API.Enums;

namespace Inventory.API.Models;
public class StockMovement
{
    public int MovementId { get; set; }  // Identifiant unique du mouvement de stock
    public int StockItemId { get; set; }  // Référence vers l'article de stock
    public MovementType MovementType { get; set; }  // Type de mouvement (enum)
    public int Quantity { get; set; }  // Quantité du mouvement (positive ou négative)
    public DateTime MovementDate { get; set; } = DateTime.UtcNow;  // Date du mouvement
    public string? Reason { get; set; }  // Raison du mouvement (ex: commande client, retour fournisseur)
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // Date de création de l'enregistrement
}

