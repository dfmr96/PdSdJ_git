using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public enum InventoryStates
{
    OnSlots,
    OnActions,
    OnCombining,
    OnItemPickUpPrompt
}
public class InventoryController : MonoBehaviour
{
    [SerializeField] private InventoryStates state;
    
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameObject firstSlot;
    [SerializeField] private InventorySlotViewer[] slots;
    [SerializeField] private GameObject actionPanel;
    
    [SerializeField] private GameObject pickUpPromptPanel;
    [SerializeField] private GameObject pickUpYES;
    [SerializeField] private GameObject pickUpNO;
    [SerializeField] private TMP_Text pickUpPromptText;
    
    
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

    private void Awake()
    {
        InitSlotReferences();
        inventoryPanel.SetActive(false);
        actionPanel.SetActive(false);
    }
    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        if (inventoryPanel.activeSelf)
        {
            Time.timeScale = 0;
            //Debug.Log($"{Time.timeScale}");
            inventory.Reorder();
            RefreshSlots();
            selector.SetSelectedGameObject(firstSlot);
            return;
        }
        Time.timeScale = 1;
        //Debug.Log($"{Time.timeScale}");
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
        inventory.Reorder();
        RefreshSlots();
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

    public void PickUpPrompt(InventoryItemData inventoryItemData, GameObject groundItem)
    {
        inventoryPanel.SetActive(true);
        pickUpPromptPanel.SetActive(true);
        selector.SetSelectedGameObject(pickUpYES);
        pickUpPromptText.SetText($"Do you want to take {inventoryItemData.name}?");
        Button yesButton = pickUpYES.GetComponent<Button>();
        Button noButton = pickUpNO.GetComponent<Button>();
        yesButton.onClick.AddListener(() =>
        {
            inventory.AddItem(inventoryItemData, 10);
            yesButton.onClick.RemoveAllListeners();
            noButton.onClick.RemoveAllListeners();
            pickUpPromptPanel.SetActive(false);
            ToggleInventory();
            Destroy(groundItem);
        });
        noButton.onClick.AddListener(() =>
        {
            yesButton.onClick.RemoveAllListeners();
            noButton.onClick.RemoveAllListeners();
            pickUpPromptPanel.SetActive(false);
            ToggleInventory();
        });
        //Debug.Log("Listener Added");
    }
}