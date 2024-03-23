using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
[CreateAssetMenu(menuName = "Inventory", fileName = "New Inventory")]
public class Inventory : ScriptableObject
{
    public List<InventoryItem> heldItems;
    
    public void AddItem(InventoryItemData itemData, int amount)
    {
        for (int i = 0; i < heldItems.Count; i++)
        {
            if (heldItems[i].inventoryItemData == itemData)
            {
                InventoryItem tempItem = heldItems[i];
                Debug.Log($"Se encontró {itemData.name}, tiene {heldItems[i].amount}");
                tempItem.amount += amount;
                heldItems[i] = tempItem;
                Debug.Log($"{itemData.name}, ahora tiene {heldItems[i].amount}");
                return;
            }
        }
        Debug.Log($"No se encontró ningun {itemData.name}");
        for (int i = 0; i < heldItems.Count; i++) 
        {
            if (heldItems[i].inventoryItemData == null)
            {
                InventoryItem tempItem;
                tempItem.amount = amount;
                tempItem.inventoryItemData = itemData;
                heldItems[i] = tempItem;
                Debug.Log($"Se ha creado {itemData.name}, con {itemData.amount} unidades");
                return;
            }
        }
        
    }
[ContextMenu("Empty inventory")]
    public void EmptyInventory()
    {
        for (int i = 0; i < heldItems.Count; i++)
        {
            heldItems[i] = new InventoryItem();
        }
    }
}

[Serializable]
public struct InventoryItem
{
    public int amount;
    public InventoryItemData inventoryItemData;
}
