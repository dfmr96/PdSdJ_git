using Inventory.Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory
{
    public class InventorySlotViewer : MonoBehaviour , ISelectHandler, ISubmitHandler
    {
        [SerializeField] private Inventory inventory;
        [SerializeField] private int slot;
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text amount;
        [SerializeField] private SlotsController _slotsController;
        public void SetSlotController(SlotsController slotsController)
        {
            _slotsController = slotsController;
        } 
        public InventoryItem GetInventoryItem()
        {
            return inventory.heldItems[slot];
        }
        [ContextMenu("RefreshData")]
        public void RefreshData()
        {
            if (GetInventoryItem().inventoryItemData == null)
            {
                image.sprite = null;
                amount.SetText(string.Empty);
                return;
            };
        
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
            _slotsController.SelectItem(inventory.GetItem(slot));
            Debug.Log("Slot Selected");
        }
    
        public void OnSubmit(BaseEventData eventData)
        {
            //inventoryController.SetSelectedItem(inventory.GetItem(slot));
        }
    }
}