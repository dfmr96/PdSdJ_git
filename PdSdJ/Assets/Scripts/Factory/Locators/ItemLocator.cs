using System;
using Inventory;
using Unity.VisualScripting;
using UnityEngine;

namespace Factory
{
    public class ItemLocator : ObjectLocator
    {
        [field: SerializeField] public InventoryItemData ItemData { get; private set; }
        [field: SerializeField] public int Amount { get; private set; }

        private void Awake()
        {
            ObjectName = ItemData.name;
        }
    }
}