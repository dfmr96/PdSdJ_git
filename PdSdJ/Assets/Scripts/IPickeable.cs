namespace ScriptableObjects.Inventory
{
    public interface IPickeable
    {
        InventoryItemData PickUp(InventoryController inventoryController);
    }
}