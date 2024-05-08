using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Inventory.Controllers
{
    public class PickUpController : MonoBehaviour
    {
        [SerializeField] private GameObject pickUpPromptPanel;
        [SerializeField] private Button yesButton;
        [SerializeField] private Button noButton;
        [SerializeField] private TMP_Text pickUpPromptText;
        [SerializeField] private Image itemImage;
        [SerializeField] private SelectorView _selectorView;
        [SerializeField] private InventoryController InventoryController;

        private void Awake()
        {
            _selectorView = InventoryController.Selector;
        }

        private void OnEnable()
        {
            _selectorView.SetSelectedGameObject(yesButton.gameObject);
        }

        public void BuildPrompt(GroundItem groundItem)
        {
            itemImage.sprite = groundItem.InventoryItemData.sprite;
            pickUpPromptText.SetText($"Do you want to take {groundItem.InventoryItemData.name}?");
            yesButton.onClick.AddListener(() =>
            {
                InventoryController.Inventory.AddItem(groundItem.InventoryItemData, groundItem.Amount);
                yesButton.onClick.RemoveAllListeners();
                noButton.onClick.RemoveAllListeners();
                gameObject.SetActive(false);
                InventoryController.ToggleInventory();
                Destroy(groundItem.transform.parent.gameObject);
            });
            noButton.onClick.AddListener(() =>
            {
                yesButton.onClick.RemoveAllListeners();
                noButton.onClick.RemoveAllListeners();
                gameObject.SetActive(false);
                InventoryController.ToggleInventory();
            });
        }
    }
}