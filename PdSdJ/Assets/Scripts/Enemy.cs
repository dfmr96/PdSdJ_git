using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Enemies;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyStates
{
    Idle,
    Patrol,
    Chase,
    Attack,
    Dead
}
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private PlayerDetector agro;
    [SerializeField] private float agroRadius;
    [SerializeField] private SphereCollider agroCol;
    
    [SerializeField] private EnemyStates state;
    [SerializeField] private NavMeshAgent enemyAgent;
    [SerializeField] private float speed;

    [SerializeField] private NavMeshAgent playerAgent;
    [SerializeField] private float damage;
    [field: SerializeField] public float Health { get; private set; }
    
    public event Action<Enemy> OnEnemyKilled;

    private void InitializeData()
    {
        Health = enemyData.Health;
        speed = enemyData.Speed;
        damage = enemyData.Damage;
    }

    private void Awake()
    {
        InitializeData();
    }

    private void Start()
    {
        agroCol.radius = agroRadius;
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAgent.speed = speed;
        state = EnemyStates.Idle;
        agro.OnPlayerDetected += ChasePlayer;
    }

    private void Update()
    {
        switch (state)
        {
            case EnemyStates.Idle:
                break;
            case EnemyStates.Patrol:
                break;
            case EnemyStates.Chase:
                if (playerAgent == null) return;
                
                enemyAgent.SetDestination(playerAgent.transform.position);
                break;
            case EnemyStates.Attack:
                break;
            case EnemyStates.Dead:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void ChasePlayer(NavMeshAgent agent)
    {
        if (agent.TryGetComponent(out CharacterController character) == false) return;
        playerAgent = agent;
        if (playerAgent == null)
        {
            state = EnemyStates.Idle;
            Debug.Log("Idle state");
            enemyAgent.isStopped = true;
            return;
        }
        enemyAgent.isStopped = false;
        state = EnemyStates.Chase;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            OnEnemyKilled?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
