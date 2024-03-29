using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameObject firstSlot;
    [SerializeField] private InventorySlotViewer[] slots;
    [SerializeField] private GameObject actionPanel;
    [SerializeField] private List<Button> actionButtons;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemDescription;

    [SerializeField] private bool isCombinating = false;

    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private SelectorView selector;

    private void OnEnable()
    {
        SelectFirstSlot();
    }

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);

        if (inventoryPanel.activeSelf) selector.SetSelectedGameObject(firstSlot);
    }

    public void OnItemSelected(InventoryItem inventoryItem)
    {
        inventory.SetSelectedItem(inventoryItem);
        if (inventoryItem.inventoryItemData == null)
        {
            ClearItemNameAndDescription();
            return;
        }

        SetItemNameAndDescription(inventoryItem.inventoryItemData);
    }

    private void InitSlotReferences()
    {
        foreach (InventorySlotViewer slot in slots)
        {
            slot.SetInventoryController(this);
        }
    }

    private void OnSlotSubmit()
    {
        if (!isCombinating)
        {
            ToggleActionPanel(true);
        }
        else
        {
            Debug.Log($"Item B {inventory.itemToCombineB} a combinar seleccionado");
            inventory.SetCombineItemB();
            OnCombineItem();
        }
    }

    public void SetSelectedItem(InventoryItem item)
    {
        Debug.Log($"{inventory.selectedItem.inventoryItemData.name} seleccionado");

        OnSlotSubmit();
    }

    public void UseItem()
    {
        inventory.Use();
        ToggleActionPanel(false);
    }

    public void CombineAction()
    {
        isCombinating = true;
        inventory.SetCombineItemA();
        ToggleActionPanel(false);
        Debug.Log("Item A a combinar seleccionado");
        SelectFirstSlot();
    }

    public void RefreshSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RefreshData();
        }
    }

    private void Awake()
    {
        InitSlotReferences();
        inventoryPanel.SetActive(false);
        actionPanel.SetActive(false);
    }

    private void OnCombineItem()
    {
        inventory.CombineItems();
        isCombinating = false;
        SelectFirstSlot();
        RefreshSlots();
    }

    private void ToggleActionPanel(bool value)
    {
        actionPanel.SetActive(value);

        if (value)
        {
            SelectFirstAction();
        }
        else
        {
            SelectFirstSlot();
        }
    }
    private void SelectFirstSlot()
    {
        selector.SetSelectedGameObject(firstSlot);
    }
    private void SelectFirstAction()
    {
        selector.SetSelectedGameObject(actionButtons[0].gameObject);
    }
    private void SetItemNameAndDescription(InventoryItemData inventoryItemData)
    {
        itemName.SetText(inventoryItemData.name);
        itemDescription.SetText(inventoryItemData.description);
    }
    private void ClearItemNameAndDescription()
    {
        itemName.ClearMesh();
        itemDescription.ClearMesh();
    }
}