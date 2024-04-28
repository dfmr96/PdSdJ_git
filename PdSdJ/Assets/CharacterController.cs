using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Inventory;
using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private int walkingSpeed;
    [SerializeField] private int runSpeed;
    [SerializeField] private int pushingSpeed;
    [SerializeField] private int angularSpeed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private bool isPushing = false;
    
    [field: SerializeField] public int currentSpeed { get; private set; }
    [field: SerializeField] public NavMeshAgent agent { get; private set; }

    [Header("Interaction")]
    [SerializeField] private LayerMask interactuableObjects;
    [SerializeField] private int interactionRayLength;
    [SerializeField] private InventoryController inventoryController;

    private void Start()
    {
        currentSpeed = walkingSpeed;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    public void PushObject(bool canPush)
    {
        if (canPush)
        {
            isPushing = true;
            currentSpeed = pushingSpeed;
        }
        else
        {
            currentSpeed = walkingSpeed;
            isPushing = false;
        }
    }

    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.F)) Interact();
        
        if (Input.GetKeyDown(KeyCode.I)) ToggleInventory();
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        agent.Move(transform.forward * (vertical * currentSpeed * Time.deltaTime));
        transform.Rotate(Vector3.up * (horizontal * angularSpeed * Time.deltaTime));
        //transform.Translate(Vector3.forward * (vertical * currentSpeed * Time.deltaTime));
        //rb.MovePosition(transform.forward * (vertical * currentSpeed * Time.fixedDeltaTime));
        //rb.MoveRotation(Quaternion.Euler(0,0,horizontal * angularSpeed * Time.fixedDeltaTime));
        //agent.
    }

    private void FixedUpdate()
    {
        //Move();
    }

    private void Interact()
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

    private void ToggleInventory()
    {
        inventoryController.ToggleInventory();
    }
}