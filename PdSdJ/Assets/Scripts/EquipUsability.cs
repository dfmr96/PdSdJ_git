using UnityEngine;

[CreateAssetMenu(menuName = "UsabilityData/EquipUsability", fileName = "New EquipUsability", order = 0)]
public class EquipUsability : UsabilityData
{
    public global::Inventory inventory;
    public override void Use()
    {
        inventory.weaponEquipped = inventory.selectedItem.inventoryItemData as WeaponItemData;
        if (inventory.weaponEquipped != null) Debug.Log($"{inventory.weaponEquipped.name} equipada");
    }
}