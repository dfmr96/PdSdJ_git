using UnityEngine;

public class GroundItem: MonoBehaviour, IPickeable
{
    [SerializeField] private InventoryItemData inventoryItemData;

    public InventoryItemData PickUp(InventoryController inventoryController)
    {
        inventoryController.ToggleInventory();
            
        return inventoryItemData;
    }
}