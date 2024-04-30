using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Enemies;
using State.Enemy;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
public class EnemyController : MonoBehaviour
{
    [field: SerializeField] public PlayerDetector PlayerDetector { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public float Health { get; private set; }
    [field: SerializeField] public EnemyStateMachine EnemyStateMachine { get; private set; }
    
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private float speed;
    
    
    [SerializeField] private float agroRadius;
    [SerializeField] private SphereCollider agroCol;


    public event Action<EnemyController> OnEnemyKilled;

    private void InitializeData()
    {
        Health = enemyData.Health;
        speed = enemyData.Speed;
    }

    private void Awake()
    {
        InitializeData();
    }

    private void Start()
    {
        agroCol.radius = agroRadius;
        Agent = GetComponent<NavMeshAgent>();
        Agent.speed = speed;
        EnemyStateMachine = new EnemyStateMachine(this);
    }

    private void Update()
    {
        EnemyStateMachine.Update();
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
