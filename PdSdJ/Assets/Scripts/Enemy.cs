using System;
using System.Collections;
using System.Collections.Generic;
using Factory;
using ScriptableObjects.Enemies;
using State.Enemy;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
public class Enemy : MonoBehaviour , IEnemyProduct, IDamageable
{
    [field: SerializeField] public PlayerDetector PlayerDetector { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public float Health { get; private set; }
    [field: SerializeField] public EnemyStateMachine EnemyStateMachine { get; private set; }

    [field: SerializeField] public EnemyData EnemyData { get; private set; }
    [SerializeField] private float speed;
    
    
    [SerializeField] private float agroRadius;
    [SerializeField] private SphereCollider agroCol;


    public event Action<Enemy> OnEnemyKilled;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        Health = EnemyData.Health;
        speed = EnemyData.Speed;
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

    private void OnDisable()
    {
        OnEnemyKilled?.Invoke(this);
    }
}