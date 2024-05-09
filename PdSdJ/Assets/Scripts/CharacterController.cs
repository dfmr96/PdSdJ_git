using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using Inventory.Controllers;
using ScriptableObjects.Player;
using State.Player;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.AI;
public class CharacterController : MonoBehaviour, IDamageable
{
    [field: SerializeField] public int CurrentSpeed { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public PlayerData PlayerStats { get; private set; }

    [field: SerializeField] public AutoAim AutoAim { get; private set; }
    [field: SerializeField] public EnemiesDetector EnemiesDetector { get; private set; }
    
    [field: SerializeField] public WeaponItemData WeaponHeld { get; private set; }
    [field: SerializeField] public PushInteractor PushInteractor { get; private set; }
    
    [field: SerializeField] public PlayerStateMachine StateMachine { get; private set; }

    [field: SerializeField] public GameObject weaponHeld { get; private set; }
    [field: SerializeField] public BulletCasingsPool BulletCasingsPool { get; private set; }

    [SerializeField] private float currentHealth;

    [Header("Interaction")]
    [SerializeField] private LayerMask interactuableObjects;
    [SerializeField] private int interactionRayLength;
    [SerializeField] private InventoryController inventoryController;

    private Rigidbody rb;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Agent = GetComponent<NavMeshAgent>();
        StateMachine = new PlayerStateMachine(this);
    }

    void Update()
    {
        StateMachine.Update();
        
        if (Input.GetKeyDown(KeyCode.F)) Interact();
        
        if (Input.GetKeyDown(KeyCode.I)) ToggleInventory();
        
    }



    public IInteractuable Interact()
    {
        Debug.DrawRay(transform.position, transform.forward * interactionRayLength, Color.red, 0.25f);
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactionRayLength, interactuableObjects))
        {
            if (hit.collider.TryGetComponent(out IInteractuable interactuable))
            {
                interactuable.Interact();
                return interactuable;
            }
            
            if (hit.collider.TryGetComponent(out IPickeable pickeable))
            {
                pickeable.PickUp(inventoryController, this);
            }
        }
        Debug.Log("Interactuar llamado");
        return default;
    }

    public void ToggleInventory()
    {
        inventoryController.ToggleInventory();
    }

    public void TakeDamage(float damageTaken)
    {
        Debug.Log($"Player dañado con {damageTaken}");
    }

    private void LateUpdate()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}