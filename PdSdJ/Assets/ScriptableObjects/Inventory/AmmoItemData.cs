using UnityEngine;

namespace ScriptableObjects.Inventory
{
    [CreateAssetMenu(menuName = "ItemData/AmmoItemData", fileName = "New AmmoItemData", order = 0)]
    public class AmmoItemData: InventoryItemData
    {
        public WeaponItemData compatibleWeapon;
    }
}