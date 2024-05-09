using Inventory;
using Inventory.Controllers;
using UnityEngine;

public class GroundItem: MonoBehaviour, IPickeable
{
    [field: SerializeField] public InventoryItemData InventoryItemData { get; private set; }
    [field: SerializeField] public int Amount { get; private set; }
    public void PickUp(InventoryController inventoryController, CharacterController player)
    {
        inventoryController.EnablePickUpPrompt(this, player);
    }
}