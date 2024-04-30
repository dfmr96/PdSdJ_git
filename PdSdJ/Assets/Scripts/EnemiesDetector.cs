using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemiesDetector : MonoBehaviour
{
    [field: SerializeField] public List<EnemyController> enemiesInRange { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyController enemy))
        {
            if (!enemiesInRange.Contains(enemy))
            {
                enemy.OnEnemyKilled += RemoveEnemyKilled;
                enemiesInRange.Add(enemy);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out EnemyController enemy))
        {
            if (enemiesInRange.Contains(enemy))
            {
                enemy.OnEnemyKilled -= RemoveEnemyKilled;
                enemiesInRange.Remove(enemy);
            }
        }
    }

    private void RemoveEnemyKilled(EnemyController enemyControllerKilled)
    {
        enemiesInRange.Remove(enemyControllerKilled);
    }
}
