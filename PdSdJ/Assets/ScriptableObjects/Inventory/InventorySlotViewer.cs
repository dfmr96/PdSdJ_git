using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotViewer : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private int slot;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text amount;


    public InventoryItem GetInventoryItem()
    {
        return inventory.heldItems[slot];
    }
    
    [ContextMenu("RefreshData")]
    public void RefreshData()
    {
        if (GetInventoryItem().inventoryItemData == null) return;
        
        InventoryItem slotItem = GetInventoryItem();
        
        image.sprite = slotItem.inventoryItemData.sprite;
        amount.SetText($"{slotItem.amount}");
    }

    private void OnEnable()
    {
        RefreshData();
    }

}
