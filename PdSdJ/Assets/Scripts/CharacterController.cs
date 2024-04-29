using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Player;
using State.Player;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.AI;


public enum PlayerStates
{
    Idle,
    Walking,
    Running,
    Pushing,
    Interacting,
    Aiming,
    Grabbed,
    Dead
}
public class CharacterController : MonoBehaviour
{
    [field: SerializeField] public int CurrentSpeed { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public PlayerData PlayerStats { get; private set; }

    [SerializeField] private Rigidbody rb;
    [SerializeField] private bool isPushing = false;
    [SerializeField] private CapsuleCollider aimRange;
    [SerializeField] private EnemiesDetector enemiesDetector;
    [SerializeField] private AutoAim autoAim;
    

    [Header("Interaction")]
    [SerializeField] private LayerMask interactuableObjects;
    [SerializeField] private int interactionRayLength;
    [SerializeField] private InventoryController inventoryController;

    [SerializeField] private PlayerStates state;

    [SerializeField] private PlayerStateMachine playerStateMachine;

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        state = PlayerStates.Idle;
        playerStateMachine = new PlayerStateMachine(this);
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
        playerStateMachine.Update();
        /*float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal == 0 && vertical == 0) state = PlayerStates.Idle;
        else state = PlayerStates.Walking;

        if (Input.GetKeyDown(KeyCode.X))
        {
            Aim();
        } ; //TODO
        if (Input.GetKey(KeyCode.X))
        {
            state = PlayerStates.Aiming;
        }
        
        if (Input.GetKeyDown(KeyCode.F)) Interact();
        
        if (Input.GetKeyDown(KeyCode.I)) ToggleInventory();

        switch (state)
        {
            case PlayerStates.Idle:
                break;
            case PlayerStates.Walking:
                Move(horizontal, vertical);
                break;
            case PlayerStates.Running:
                break;
            case PlayerStates.Pushing:
                break;
            case PlayerStates.Interacting:
                break;
            case PlayerStates.Aiming:
                //aimRange.enabled = true;

                if (Input.GetKeyDown(KeyCode.C)) Shoot();
                break;
            case PlayerStates.Grabbed:
                break;
            case PlayerStates.Dead:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }*/
    }

    public void Aim()
    {
        Enemy closestEnemy = autoAim.TryGetClosestEnemy();

        if (closestEnemy != null)
        {
            transform.LookAt(closestEnemy.transform);
        }
    }

    private void Shoot()
    {
        Enemy closestEnemy = null;
        float distance = 0;
        List<Enemy> enemiesInRange = enemiesDetector.enemiesInRange;
        for (int i = 0; i < enemiesInRange.Count; i++)
        {
            float enemyDistance = (enemiesInRange[i].transform.position - transform.position).magnitude;
            if (distance == 0 || enemyDistance < distance)
            {
                distance = enemyDistance;
                closestEnemy = enemiesInRange[i];
            }
        }

        if (closestEnemy != null)
        {
            Debug.Log($"{closestEnemy} took 1 damage at {distance}m");
            closestEnemy.TakeDamage(1);
        }
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