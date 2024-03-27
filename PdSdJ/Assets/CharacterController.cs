using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Inventory;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private int walkingSpeed;
    [SerializeField] private int runSpeed;
    [SerializeField] private int angularSpeed;
    [SerializeField] private int currentSpeed;

    [Header("Interaction")]
    [SerializeField] private LayerMask interactuableObjects;
    [SerializeField] private int interactionRayLength;
    [SerializeField] private InventoryController inventoryController;

    private void Start()
    {
        currentSpeed = walkingSpeed;
    }

    void Update()
    {
        Move();
        Interact();
        
        if (Input.GetKeyDown(KeyCode.I)) ToggleInventory();
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        transform.Translate(Vector3.forward * (vertical * currentSpeed * Time.deltaTime));
        transform.Rotate(Vector3.up * (horizontal * angularSpeed * Time.deltaTime));
    }

    private void Interact()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.DrawRay(transform.position, transform.forward * interactionRayLength, Color.red, 0.25f);
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactionRayLength, interactuableObjects))
            {
                if (hit.collider.TryGetComponent(out IInteractuable interactuable))
                {
                    interactuable.Interact();
                }
                
                if (hit.collider.TryGetComponent(out IPickeable pickeable))
                {
                    pickeable.PickUp(inventoryController);
                }
            }
        }
    }

    private void ToggleInventory()
    {
        inventoryController.ToggleInventory();
    }
}