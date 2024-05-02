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
        private SelectorView selector;

        public event Action<InventoryItem> OnItemSelected;

        private void Awake()
        {
            selector = _inventoryController.Selector;
            SetSlotViewerReferences();
            
            _inventoryController.OnInventoryOpened += RefreshSlots;
            _inventoryController.OnInventoryOpened += SelectFirstSlot;
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
        }
    }
}