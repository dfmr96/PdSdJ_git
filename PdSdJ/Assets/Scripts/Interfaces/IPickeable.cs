using Inventory;
using Inventory.Controllers;

public interface IPickeable
{
    void PickUp(InventoryController inventoryController, CharacterController player);
}