using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Inventory.Controllers
{
    public class ActionsController : MonoBehaviour
    {
        [SerializeField] private InventoryController inventoryController;
        [SerializeField] private GameObject actionPanel;
        [SerializeField] private List<Button> actionButtons;
        private Inventory _inventory;
        private SelectorView selector;

        //public event Action OnUse;
        public event Action OnActionCalled;
        //public event Action OnCheck;
        //public event Action OnEquip;

        private void Awake()
        {
            _inventory = inventoryController.Inventory;
            selector = inventoryController.Selector;
        }

        public void CombineItem()
        {
            _inventory.SetCombineItemA();
            inventoryController.SetState(InventoryStates.OnCombining);
            OnActionCalled?.Invoke();
        }

        
        public void UseItem()
        {
            _inventory.Use();
            _inventory.Reorder();
            inventoryController.SlotsController.RefreshSlots();
            inventoryController.SetState(InventoryStates.OnSlotSelect);
            OnActionCalled?.Invoke();
        }

        public void CheckItem()
        {
            //TODO
            Debug.Log("Check Item");
        }

        private void OnEnable()
        {
            selector.SetSelectedGameObject(actionButtons[0].gameObject);
        }
    }
}