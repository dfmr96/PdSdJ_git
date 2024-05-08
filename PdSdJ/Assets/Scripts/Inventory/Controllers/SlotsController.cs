using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Inventory.Controllers
{
    public class SlotsController : MonoBehaviour
    {
        [SerializeField] private InventoryController _inventoryController;
        [SerializeField] private InventorySlotViewer firstSlot;
        [SerializeField] private InventorySlotViewer[] slots;
        private ItemDescriptionController ItemDescriptionController;
        private Inventory _inventory;
        private SelectorView selector;

        public event Action<InventoryItem> OnItemSelected;
        public event Action OnItemSubmit;

        private void Awake()
        {
            selector = _inventoryController.Selector;
            _inventory = _inventoryController.Inventory;
            IItemDescriptionProvider itemDescriptionProvider = _inventoryController;
            ItemDescriptionController = itemDescriptionProvider.ItemDescriptionController;
            
            SetSlotViewerReferences();
            
            _inventoryController.OnInventoryOpened += RefreshSlots;
            _inventoryController.OnInventoryOpened += SelectFirstSlot;
            _inventoryController.ActionsController.OnActionCalled += SelectFirstSlot;
        }
        
        private void SetSlotViewerReferences()
        {
            foreach (InventorySlotViewer slot in slots)
            {
                slot.SetSlotController(this);
            }
        }
        
        public void SelectItem(InventoryItem inventoryItem)
        {
            Inventory inventory = _inventoryController.Inventory;
            inventory.SetSelectedItem(inventoryItem);
            OnItemSelected?.Invoke(inventoryItem);
        }

        public void SelectFirstSlot()
        {
            selector.SetSelectedGameObject(firstSlot.gameObject);
            OnItemSelected?.Invoke(firstSlot.GetInventoryItem());
        }
        
        public void RefreshSlots()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].RefreshData();
            }
        }
        private void OnDestroy()
        {
            _inventoryController.OnInventoryOpened -= SelectFirstSlot;
            _inventoryController.OnInventoryOpened -= RefreshSlots;
            _inventoryController.ActionsController.OnActionCalled -= SelectFirstSlot;
        }
        public void ItemSubmit()
        {
            switch (_inventoryController.State)
            {
                case InventoryStates.OnCombining:
                    _inventory.SetCombineItemB();
                    _inventory.CombineItems();
                    SelectFirstSlot();
                    RefreshSlots();
                    _inventoryController.SetState(InventoryStates.OnSlotSelect);
                    break;
                case InventoryStates.OnSlotSelect:
                    _inventoryController.SetState(InventoryStates.OnActions);
                    OnItemSubmit?.Invoke();
                    break;
            }
        }

        private void OnEnable()
        {
            InventoryItem firstItem = _inventory.GetItem(0);
            if (firstItem.inventoryItemData != null) ItemDescriptionController.SetItemNameAndDescription(firstItem);
        }
    }
}