using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace State.Enemy
{
    public class AttackState : StateBase
    {
        private readonly EnemyStateMachine _enemyStateMachine;
        private readonly global::Enemy _enemy;
        private readonly NavMeshAgent agent;
        private readonly PlayerDetector _playerDetector;
        private float attackDuration;
        private float attackTimer;
        private float damage;
        
        public AttackState(EnemyStateMachine enemyStateMachine)
        {
            _enemyStateMachine = enemyStateMachine;
            _enemy = _enemyStateMachine.Enemy;
            _playerDetector = _enemy.PlayerDetector;
            agent = _enemy.Agent;
            attackTimer = 0;
            attackDuration = _enemy.EnemyData.AttackDuration;
            damage = _enemy.EnemyData.Damage;
        }

        public override void Enter()
        {
            agent.isStopped = true;
            if (_playerDetector.Player.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(damage);
            }
        }

        public override void Update()
        {
            attackTimer += Time.deltaTime;
            if (attackTimer > attackDuration)
            {
                _enemyStateMachine.ChangeStateTo(_enemyStateMachine.ChaseState);
            }
        }

        public override void Exit()
        {
            attackTimer = 0;
            agent.isStopped = false;
        }
    }
}