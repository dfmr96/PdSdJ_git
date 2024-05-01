using System;
using System.Collections.Generic;
using ScriptableObjects.Enemies;
using UnityEngine;

namespace Factory
{
    public class EnemyFactory : AbstractFactory<Enemy>
    {
        public List<EnemyData> availableEnemies;
        public override Enemy CreateProduct(string enemyID)
        {
            foreach (EnemyData enemyData in availableEnemies)
            {
                if (enemyID == enemyData.ID)
                {
                    return enemyData.EnemyPrefab;
                }
            }

            return default;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                Instantiate(CreateProduct("E01"));
            }
            
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                Instantiate(CreateProduct("E02"));
            }
            
            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                Instantiate(CreateProduct("E03"));
            }
        }
    }
}