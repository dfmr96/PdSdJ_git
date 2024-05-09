using Inventory;
using UnityEngine;

namespace ScriptableObjects.ItemData
{
    [CreateAssetMenu(menuName = "ItemData/AmmoItemData", fileName = "New AmmoItemData", order = 0)]
    public class AmmoItemData: InventoryItemData
    {
        public WeaponItemData compatibleWeapon;
    }
}