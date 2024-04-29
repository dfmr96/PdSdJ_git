using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Inventory;
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
    [Header("Movement")]
    [SerializeField] private int walkingSpeed;
    [SerializeField] private int runSpeed;
    [SerializeField] private int pushingSpeed;
    [SerializeField] private int angularSpeed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private bool isPushing = false;
    [SerializeField] private CapsuleCollider aimRange;
    [SerializeField] private EnemiesDetector enemiesDetector;
    [SerializeField] private AutoAim autoAim;
    
    [field: SerializeField] public int currentSpeed { get; private set; }
    [field: SerializeField] public NavMeshAgent agent { get; private set; }

    [Header("Interaction")]
    [SerializeField] private LayerMask interactuableObjects;
    [SerializeField] private int interactionRayLength;
    [SerializeField] private InventoryController inventoryController;

    [SerializeField] private PlayerStates state;

    private void Start()
    {
        currentSpeed = walkingSpeed;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        state = PlayerStates.Idle;
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
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal == 0 && vertical == 0) state = PlayerStates.Idle;
        else state = PlayerStates.Walking;
        
        if (Input.GetKeyDown(KeyCode.X)) //TODO
        if (Input.GetKey(KeyCode.X)) state = PlayerStates.Aiming;
        
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
        }
    }

    public void Aim()
    {
        
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

    private void Move(float hor, float ver)
    {

        agent.Move(transform.forward * (ver * currentSpeed * Time.deltaTime));
        transform.Rotate(Vector3.up * (hor * angularSpeed * Time.deltaTime));
        
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