using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.Controllers
{
    public class ItemDescriptionController : MonoBehaviour
    {
        [SerializeField] private InventoryController _inventoryController;
        
        [SerializeField] private Image itemSprite;
        [SerializeField] private TMP_Text itemName;
        [SerializeField] private TMP_Text itemDescription;

        private void Awake()
        {
            _inventoryController._slotsController.OnItemSelected += SetItemNameAndDescription;
        }

        private void OnDestroy()
        {
            _inventoryController._slotsController.OnItemSelected -= SetItemNameAndDescription;
        }

        public void SetItemNameAndDescription(InventoryItem inventoryItem)
        {
            InventoryItemData itemData = inventoryItem.inventoryItemData;
            if (itemData == null)
            {
                itemName.ClearMesh();
                itemDescription.ClearMesh();
            }
            else
            {
                itemName.SetText(itemData.name);
                itemDescription.SetText(itemData.description);
            }
        }
    }
}