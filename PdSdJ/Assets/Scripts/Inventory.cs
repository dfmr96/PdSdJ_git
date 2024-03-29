using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Inventory;
using UnityEditor.Timeline.Actions;
using UnityEngine;
[CreateAssetMenu(menuName = "Inventory", fileName = "New Inventory")]
public class Inventory : ScriptableObject
{
    public List<InventoryItem> heldItems;
    public WeaponItemData weaponEquipped;
    public InventoryItem selectedItem;
    public InventoryItemData combinationItemSelection;
    public CombinationTable combinationTable;
    public InventoryItem itemToCombineA;
    public InventoryItem itemToCombineB;
    
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
                tempItem.slot = i;
                heldItems[i] = tempItem;
                Debug.Log($"Se ha creado {itemData.name}, con {itemData.amount} unidades");
                return;
            }
        }
    }

    public void DeleteItem(InventoryItem inventoryItem)
    {
        InventoryItem tempInventoryItem = heldItems[inventoryItem.slot];
        tempInventoryItem.inventoryItemData = null;
        tempInventoryItem.amount = 0;
        tempInventoryItem.slot = inventoryItem.slot;
        heldItems[inventoryItem.slot] = tempInventoryItem;

    }
[ContextMenu("Empty inventory")]
    public void EmptyInventory()
    {
        for (int i = 0; i < heldItems.Count; i++)
        {
            heldItems[i] = new InventoryItem();
        }
    }
    public void CombineItems()
    {
        if (selectedItem.inventoryItemData.combinableInfo != null)
        {
            InventoryItemData newItem = selectedItem.inventoryItemData.combinableInfo.GetCombinationResult(
                itemToCombineA.inventoryItemData,
                itemToCombineB.inventoryItemData); 
            Debug.Log($"{newItem.name} encontrado");
            if (itemToCombineA.amount < 2)
            {
                DeleteItem(itemToCombineA);
                Debug.Log("ItemToCombineA borrado");
            }
            else
            {
                InventoryItem tempItem = new InventoryItem();
                {
                    tempItem.inventoryItemData = itemToCombineA.inventoryItemData;
                    tempItem.amount = itemToCombineA.amount--;
                    tempItem.slot = itemToCombineA.slot;
                }
                Debug.Log($"ItemToCombineA amount {tempItem.amount}");
                tempItem.amount = itemToCombineA.amount--;
                heldItems[itemToCombineA.slot] = tempItem;
                Debug.Log($"ItemToCombineA amount {tempItem.amount}");
            }

            if (itemToCombineB.amount < 2)
            {
                DeleteItem(itemToCombineB);
            }
            else
            {
                InventoryItem tempItem = new InventoryItem();
                {
                    tempItem.inventoryItemData = itemToCombineB.inventoryItemData;
                    tempItem.amount = itemToCombineB.amount--;
                    tempItem.slot = itemToCombineB.slot;
                }
                Debug.Log($"ItemToCombineB amount {tempItem.amount}");
                tempItem.amount = itemToCombineB.amount--;
                heldItems[itemToCombineB.slot] = tempItem;
                Debug.Log($"ItemToCombineB amount {tempItem.amount}");
            }
            AddItem(newItem,1);
            itemToCombineA = default;
            itemToCombineB = default;
        }
    }
    public void SetCombineItemA()
    {
        itemToCombineA = selectedItem;
    }
    public void SetCombineItemB()
    {
        itemToCombineB = selectedItem;
    }
    public void SetSelectedItem(InventoryItem inventoryItem)
    {
        selectedItem = inventoryItem;
    }
    public void Use()
    {
        if (selectedItem.inventoryItemData.usabilityData != null) selectedItem.inventoryItemData.usabilityData.Use();
    }
    public InventoryItem GetItem(int slot)
    {
        return heldItems[slot];
    }
}

[Serializable]
public struct InventoryItem
{
    public int slot;
    public int amount;
    public InventoryItemData inventoryItemData;
}
