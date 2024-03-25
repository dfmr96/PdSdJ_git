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

    public void OnItemSelected(InventoryItemData inventoryItemData)
    {
        if (inventoryItemData == null)
        {
            ClearItemNameAndDescription();
            return;
        }
        SetItemNameAndDescription(inventoryItemData);
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
            Debug.Log("Item B a combinar seleccionado");
            inventory.SetCombineItemB();
            OnCombineItem();
        }
    }
    public void SetSelectedItem(InventoryItemData itemData)
    {
        inventory.SetSelectedItem(itemData);
        Debug.Log($"{inventory.selectedItem.name} seleccionado");

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
        EventSystem.current.SetSelectedGameObject(firstSlot);
    }
    private void SelectFirstAction()
    {
        EventSystem.current.SetSelectedGameObject(actionButtons[0].gameObject);
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