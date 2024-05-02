using System;
using System.Collections.Generic;
using State.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.Controllers
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private InventoryStates state;
        [field: SerializeField] public InventoryStateMachine inventoryStateMachine { get; private set; }
    
        [field: SerializeField] public SlotsController _slotsController { get; private set; }

        [field: SerializeField] public Inventory Inventory { get; private set; }
        [SerializeField] private GameObject firstSlot;
        [SerializeField] private InventorySlotViewer[] slots;
    
        [field: SerializeField] public ActionsController actionsController { get; private set; }
        [SerializeField] private GameObject actionPanel;
        [SerializeField] private List<Button> actionButtons;
    
    
        [field: SerializeField] public PickUpController pickUpController { get; private set; }
        [SerializeField] private GameObject pickUpPromptPanel;
        [SerializeField] private GameObject pickUpYES;
        [SerializeField] private GameObject pickUpNO;
        [SerializeField] private TMP_Text pickUpPromptText;

        [field: SerializeField] public ItemDescriptionController ItemDescriptionController { get; private set; }
        [SerializeField] private TMP_Text itemName;
        [SerializeField] private TMP_Text itemDescription;

        [SerializeField] private bool isCombinating = false;

        [SerializeField] private GameObject inventoryPanel;
        [field: SerializeField] public SelectorView Selector { get; private set; }

        public event Action OnInventoryOpened;
        private void Awake()
        {
            inventoryPanel.SetActive(false);
        }
        public void OpenInventory()
        {
            Time.timeScale = 0;
            Inventory.Reorder();
            inventoryPanel.SetActive(true);
            OnInventoryOpened?.Invoke();
        }

        /*private void OnSlotSubmit()
        {
            if (!isCombinating)
            {
                ToggleActionPanel(true);
            }
            else
            {
                Debug.Log($"Item B {Inventory.itemToCombineB} a combinar seleccionado");
                Inventory.SetCombineItemB();
                OnCombineItem();
            }
        }*/

        /*public void SetSelectedItem(InventoryItem item)
        {
            Debug.Log($"{Inventory.selectedItem.inventoryItemData.name} seleccionado");

            OnSlotSubmit();
        }*/

        public void UseItem()
        {
            Inventory.Use();
            ToggleActionPanel(false);
            Inventory.Reorder();
            RefreshSlots();
        }

        public void CombineAction()
        {
            isCombinating = true;
            Inventory.SetCombineItemA();
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
            Inventory.CombineItems();
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
            Selector.SetSelectedGameObject(firstSlot);
        }
        private void SelectFirstAction()
        {
            Selector.SetSelectedGameObject(actionButtons[0].gameObject);
        }

        public void PickUpPrompt(GroundItem groundItem)
        {
            inventoryPanel.SetActive(true);
            pickUpPromptPanel.SetActive(true);
            Selector.SetSelectedGameObject(pickUpYES);
            pickUpPromptText.SetText($"Do you want to take {groundItem.InventoryItemData.name}?");
            Button yesButton = pickUpYES.GetComponent<Button>();
            Button noButton = pickUpNO.GetComponent<Button>();
            yesButton.onClick.AddListener(() =>
            {
                Inventory.AddItem(groundItem.InventoryItemData, groundItem.Amount);
                yesButton.onClick.RemoveAllListeners();
                noButton.onClick.RemoveAllListeners();
                pickUpPromptPanel.SetActive(false);
                OpenInventory();
                Destroy(groundItem.transform.parent);
            });
            noButton.onClick.AddListener(() =>
            {
                yesButton.onClick.RemoveAllListeners();
                noButton.onClick.RemoveAllListeners();
                pickUpPromptPanel.SetActive(false);
                OpenInventory();
            });
            //Debug.Log("Listener Added");
        }
    }
}