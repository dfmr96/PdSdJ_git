using UnityEngine;
using UnityEngine.Rendering.UI;

namespace ScriptableObjects.Inventory
{
    public class GroundItem: MonoBehaviour, IPickeable
    {
        [SerializeField] private InventoryItemData inventoryItemData;

        public InventoryItemData PickUp(InventoryController inventoryController)
        {
            inventoryController.ToggleInventory();
            return inventoryItemData;
        }
    }
}