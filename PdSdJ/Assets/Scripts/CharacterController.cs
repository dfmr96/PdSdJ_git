using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Player;
using State.Player;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.AI;
public class CharacterController : MonoBehaviour
{
    [field: SerializeField] public int CurrentSpeed { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public PlayerData PlayerStats { get; private set; }

    [field: SerializeField] public AutoAim AutoAim { get; private set; }
    [field: SerializeField] public EnemiesDetector EnemiesDetector { get; private set; }
    
    [field: SerializeField] public WeaponItemData WeaponHeld { get; private set; }
    [field: SerializeField] public PushInteractor PushInteractor { get; private set; }
    [SerializeField] private bool isPushing = false;
    [SerializeField] private CapsuleCollider aimRange;
    [SerializeField] private Rigidbody rb;


    [Header("Interaction")]
    [SerializeField] private LayerMask interactuableObjects;
    [SerializeField] private int interactionRayLength;
    [SerializeField] private InventoryController inventoryController;


    [field: SerializeField] public PlayerStateMachine StateMachine { get; private set; }

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        StateMachine = new PlayerStateMachine(this);
    }

    public void PushObject(bool canPush)
    {
        if (canPush)
        {
            isPushing = true;
            //CurrentSpeed = pushingSpeed;
        }
        else
        {
            //CurrentSpeed = walkingSpeed;
            isPushing = false;
        }
    }

    void Update()
    {
        StateMachine.Update();
        
        if (Input.GetKeyDown(KeyCode.F)) Interact();
        
        if (Input.GetKeyDown(KeyCode.I)) ToggleInventory();
        
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