using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameObject firstSlot;
    [SerializeField] private InventorySlotViewer[] slots;
    [SerializeField] private List<Button> actionButtons;

    [SerializeField] private bool isCombinating = false;
    
    public void ActiveActions()
    {
        if (!isCombinating)
        {
            EventSystem.current.SetSelectedGameObject(actionButtons[0].gameObject);
        }
        else
        {
            Debug.Log("Item B a combinar seleccionado");
            inventory.itemToCombineB = inventory.selectedItem;
            CombineItem();
        }
    }

    public void SetSelectedItem()
    {
        inventory.selectedItem = EventSystem.current.currentSelectedGameObject
            .GetComponent<InventorySlotViewer>()
            .GetInventoryItem()
            .inventoryItemData;
        Debug.Log($"{inventory.selectedItem.name} seleccionado");
        
        ActiveActions();
    }

    public void UseItem()
    {
        if (inventory.selectedItem.usabilityData != null) inventory.selectedItem.usabilityData.Use();
    }

    public void CombineAction()
    {
        isCombinating = true;
        inventory.itemToCombineA = inventory.selectedItem;
        Debug.Log("Item A a combinar seleccionado");
        EventSystem.current.SetSelectedGameObject(firstSlot);
    }

    public void CombineItem()
    {
        if (inventory.selectedItem.combinableInfo != null)
        {
            InventoryItemData newItem = inventory.selectedItem.combinableInfo.GetCombinationResult(
                inventory.itemToCombineA,
                inventory.itemToCombineB); 
            Debug.Log($"{newItem.name} encontrado");
            inventory.AddItem(newItem,1);
        }

        isCombinating = false;
        EventSystem.current.SetSelectedGameObject(firstSlot);
        inventory.itemToCombineA = null;
        inventory.itemToCombineB = null;
        RefreshSlots();
    }

    public void RefreshSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RefreshData();
        }
    }
}
