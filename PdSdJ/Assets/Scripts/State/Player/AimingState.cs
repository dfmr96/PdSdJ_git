using System.Collections.Generic;
using Pool;
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
            _controller.weaponHeld.SetActive(true);
            global::Enemy closestEnemy = _autoAim.TryGetClosestEnemy();

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
            global::Enemy closestEnemy = ClosestEnemy(out var distance);

            if (closestEnemy != null)
            {
                if (closestEnemy.gameObject.TryGetComponent(out IDamageable damageableEnemy))
                {
                    damageableEnemy.TakeDamage(weaponHeld.damage);
                    Debug.Log($"{closestEnemy} took {weaponHeld.damage} damage at {distance}m. Current Health: {closestEnemy.Health}");
                }
                else
                {
                    Debug.LogError("Enemy not damageable");
                }
            }

            GetBulletCasing();
        }

        private void GetBulletCasing()
        {
            BulletCasing bulletCasing = _controller.BulletCasingsPool._objectPool.Get();
            if (bulletCasing == null)
            {
                return;
            }
            bulletCasing.Deactivate();
        }

        private global::Enemy ClosestEnemy(out float distance)
        {
            global::Enemy closestEnemy = null;
            distance = 0;
            List<global::Enemy> enemiesInRange = enemiesDetector.enemiesInRange;
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
            _controller.weaponHeld.SetActive(false);
        }
    }
}