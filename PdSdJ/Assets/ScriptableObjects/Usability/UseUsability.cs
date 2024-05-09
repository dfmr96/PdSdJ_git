using System;
using Inventory;
using ScriptableObjects.ItemData;
using Unity.VisualScripting;
using UnityEngine;

namespace ScriptableObjects.Usability
{
    [CreateAssetMenu(fileName = "New UseUsability", menuName = "UsabilityData/UseUsability", order = 0)]
    public class UseUsability : UsabilityData
    {
        private InventoryItemData _itemData;
        private CharacterController _characterController;
        
        public override void Use()
        {
            IInteractuable objectToInteract = _characterController.Interact();

            if (objectToInteract is IItemRequire)
            {
                IItemRequire itemRequire = (IItemRequire)objectToInteract;
                itemRequire.RequireItemUsed(_itemData);
            }
        }

        public void SetCharacterController(CharacterController player)
        {
            _characterController = player;
            Debug.Log($"Player controller seteado");
        }

        public void SetItemData(InventoryItemData itemData)
        {
            _itemData = itemData;
        }
    }
}