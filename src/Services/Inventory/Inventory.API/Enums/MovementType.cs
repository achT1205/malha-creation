namespace Inventory.API.Enums;

public enum MovementType
{
    Entry,      // Entrée de stock (ex: réapprovisionnement)
    Exit,       // Sortie de stock (ex: vente)
    Adjustment, // Ajustement (ex: correction d'inventaire)
    Return      // Retour (ex: retour client)
}
