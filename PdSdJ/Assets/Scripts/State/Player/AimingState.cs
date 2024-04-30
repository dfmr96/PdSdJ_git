using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace State.Player
{
    public class AimingState : StateBase
    {
        private PlayerStateMachine playerStateMachine;
        private AutoAim _autoAim;
        private CharacterController _controller;
        private EnemiesDetector enemiesDetector;
        private WeaponItemData weaponHeld;

        public AimingState(PlayerStateMachine playerStateMachine)
        {
            this.playerStateMachine = playerStateMachine;
            _controller = playerStateMachine.Controller;
            _autoAim = _controller.AutoAim;
            enemiesDetector = _controller.EnemiesDetector;
            weaponHeld = _controller.WeaponHeld;
        }
        public override void Enter()
        {
            Enemy closestEnemy = _autoAim.TryGetClosestEnemy();

            if (closestEnemy != null)
            {
                _controller.transform.LookAt(closestEnemy.transform);
            }
        }

        public override void Update()
        {
            if (Input.GetButtonUp("Aim"))
            {
                playerStateMachine.ChangeStateTo(playerStateMachine.IdleState);
            }
            
            
            if (Input.GetButtonDown("Shoot"))
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            Enemy closestEnemy = ClosestEnemy(out var distance);

            if (closestEnemy != null)
            {
                closestEnemy.TakeDamage(weaponHeld.damage);
                Debug.Log($"{closestEnemy} took {weaponHeld.damage} damage at {distance}m. Current Health: {closestEnemy.Health}");
            }
        }

        private Enemy ClosestEnemy(out float distance)
        {
            Enemy closestEnemy = null;
            distance = 0;
            List<Enemy> enemiesInRange = enemiesDetector.enemiesInRange;
            for (int i = 0; i < enemiesInRange.Count; i++)
            {
                float enemyDistance = (enemiesInRange[i].transform.position - _controller.transform.position).magnitude;
                if (distance == 0 || enemyDistance < distance)
                {
                    distance = enemyDistance;
                    closestEnemy = enemiesInRange[i];
                }
            }

            return closestEnemy;
        }

        public override void Exit()
        {
            //throw new System.NotImplementedException();
        }
    }
}