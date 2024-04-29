using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class AutoAim : MonoBehaviour
{
    [SerializeField] private EnemiesDetector enemiesDetector;
    //[SerializeField] private List<Enemy> enemiesInRange;

    public Enemy TryGetClosestEnemy()
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

        if (closestEnemy != null) return closestEnemy;
        return null;
    }
}
