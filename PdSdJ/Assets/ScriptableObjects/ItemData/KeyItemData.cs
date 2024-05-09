using System;
using Inventory;
using ScriptableObjects.Usability;
using UnityEngine;

namespace ScriptableObjects.ItemData
{
    [CreateAssetMenu(menuName = "ItemData/KeyItemData", fileName = "New KeyItemData", order = 0)]
    public class KeyItemData : InventoryItemData
    {
        public int id;
    
        private void Awake()
        {
            UseUsability useUsability = (UseUsability)usabilityData;
            useUsability.SetItemData(this);
            Debug.Log("Item data seteada");
        }
    }
}