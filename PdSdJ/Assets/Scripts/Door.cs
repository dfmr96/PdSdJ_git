using System.Collections;
using System.Collections.Generic;
using Inventory;
using ScriptableObjects.ItemData;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour, IInteractuable, IItemRequire
{
    [SerializeField] private Transform moveTo;
    [SerializeField] private PlayerDetector playerDetector;
    [SerializeField] private BoxCollider col;
    [SerializeField] private bool isClosed;
    [SerializeField] private KeyItemData keyRequired;
    public void Interact()
    {
        if (isClosed)
        {
            Debug.Log("Door is closed");
            return;
        };
        if (playerDetector.Player != null)
        {
            NavMeshAgent player = playerDetector.Player.Agent;
            player.Warp(moveTo.position);
        }
    }

    public void UnlockDoor()
    {
        Debug.Log("Door unlocked");
        isClosed = false;
        Interact();
        playerDetector.Player.ToggleInventory();
    }

    public void RequireItemUsed(InventoryItemData inventoryItemData)
    {
        if (inventoryItemData is KeyItemData)
        {
            KeyItemData keyUsed = (KeyItemData)inventoryItemData;
            if (keyUsed.id == keyRequired.id)
            {
                UnlockDoor();
            }
            else
            {
                Debug.Log("Wrong key used");
            }
        }
    }
}

public interface IItemRequire
{
    void RequireItemUsed(InventoryItemData inventoryItemData);
}
