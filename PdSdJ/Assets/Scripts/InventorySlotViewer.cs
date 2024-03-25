using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotViewer : MonoBehaviour , ISelectHandler, ISubmitHandler
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private int slot;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text amount;
    [SerializeField] private InventoryController inventoryController;
    public void SetInventoryController(InventoryController inventoryController)
    {
        this.inventoryController = inventoryController;
    } 
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
    
    public void OnSelect(BaseEventData eventData)
    {
        inventoryController.OnItemSelected(inventory.GetItem(slot));
    }
    
    public void OnSubmit(BaseEventData eventData)
    {
        inventoryController.SetSelectedItem(inventory.GetItem(slot));
    }
}