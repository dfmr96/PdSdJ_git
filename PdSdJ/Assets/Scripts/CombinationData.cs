using System;
using UnityEngine;

namespace ScriptableObjects.Inventory
{
    [CreateAssetMenu(menuName = "ItemData/CombinationData", fileName = "New Combination Data", order = 0)]
    public class CombinationData : ScriptableObject
    {
        public InventoryItemData itemA;
        public InventoryItemData itemB;
        public InventoryItemData combinationResult;
    }
}