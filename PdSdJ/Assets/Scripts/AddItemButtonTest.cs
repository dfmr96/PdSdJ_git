using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using TMPro;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class AddItemButtonTest : MonoBehaviour
{
    [SerializeField] private InventoryItem itemToAdd;
    [SerializeField] private Inventory.Inventory inventory;
    [SerializeField] private TMP_Text text;


    private void Start()
    {
        RefreshButton();
    }

    [ContextMenu("Refresh Button")]
    public void RefreshButton()
    {
        text.SetText($"Agregar {itemToAdd.amount} {itemToAdd.inventoryItemData.name}");
    }

    public void AddItem()
    {
        inventory.AddItem(itemToAdd.inventoryItemData, itemToAdd.amount);
    }
}
