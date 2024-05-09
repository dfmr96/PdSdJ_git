using System;
using State.Inventory;
using UnityEngine;

namespace Inventory.Controllers
{
    public enum InventoryStates
    {
        Closed,
        OnSlotSelect,
        OnActions,
        OnCombining,
        OnItemPickUpPrompt
    }
    public class InventoryController : MonoBehaviour , ISlotProvider, IActionProvider, IPickUpProvider,IItemDescriptionProvider
    {
        [field: SerializeField] public InventoryStates State { get; private set; }
        [field: SerializeField] public InventoryStateMachine InventoryStateMachine { get; private set; }
        [field: SerializeField] public SlotsController SlotsController { get; private set; }
        [field: SerializeField] public Inventory Inventory { get; private set; }
        [field: SerializeField] public ActionsController ActionsController { get; private set; }
        [field: SerializeField] public PickUpController PickUpController { get; private set; }
        [field: SerializeField] public ItemDescriptionController ItemDescriptionController { get; private set; }

        [field: SerializeField] public SelectorView Selector { get; private set; }
        
        [SerializeField] private GameObject inventoryPanel;
        [SerializeField] private GameObject actionPanel;
        [SerializeField] private GameObject pickUpPromptPanel;
        
        public event Action OnInventoryOpened;
        private void Awake()
        {
            inventoryPanel.SetActive(false);
        }

        private void Start()
        {
            SlotsController.OnItemSubmit += OpenActionPanel;
            //ActionsController.OnUse += CloseActionPanel;
            ActionsController.OnActionCalled += CloseActionPanel;
        }

        private void Update()
        {
            
        }

        private void CloseInventory()
        {
            Time.timeScale = 1;
            inventoryPanel.SetActive(false);
            State = InventoryStates.Closed;
        }

        public void OpenInventory()
        {
            Time.timeScale = 0;
            Inventory.Reorder();
            inventoryPanel.SetActive(true);
            State = InventoryStates.OnSlotSelect;
            OnInventoryOpened?.Invoke();
        }

        public void ToggleInventory()
        {
            if (!inventoryPanel.activeSelf)
            {
                Debug.Log("Inventory Opened");
                OpenInventory();
            }
            else
            {
                CloseInventory();
                Debug.Log("Inventory Closed");
            }
        }

        public void OpenActionPanel()
        {
            actionPanel.SetActive(true);
        }

        public void CloseActionPanel()
        {
            actionPanel.SetActive(false);
        }

        private void OnDestroy()
        {
            SlotsController.OnItemSubmit -= OpenActionPanel;
            //ActionsController.OnUse -= CloseActionPanel;
            ActionsController.OnActionCalled -= CloseActionPanel;
        }

        public void EnablePickUpPrompt(GroundItem groundItem, CharacterController player)
        {
            inventoryPanel.SetActive(true);
            pickUpPromptPanel.SetActive(true);
            PickUpController.BuildPrompt(groundItem, player);
        }

        public void SetState(InventoryStates state)
        {
            State = state;
        }
    }
}