using System;
using System.Collections.Generic;
using ScriptableObjects.Enemies;
using UnityEngine;

namespace Factory
{
    public class LocatorController : MonoBehaviour
    {
        [SerializeField] private List<ObjectLocator> objectsToLocate;
        [SerializeField] private EnemyFactory _enemyFactory;

        private void Start()
        {
            PopulateList();

            foreach (ObjectLocator objectLocator in objectsToLocate)
            {
                if (objectLocator is EnemyLocator enemyLocator)
                {
                    Enemy newEnemy = _enemyFactory.CreateProduct(enemyLocator.Enemy.ID);
                    Instantiate(newEnemy); 
                    Transform enemyTransform = newEnemy.transform;
                    Transform objectLocatorTransform = enemyLocator.transform;
                    enemyTransform.position = objectLocatorTransform.position;
                    enemyTransform.rotation = objectLocatorTransform.rotation;
                    Debug.Log("Enemy created");
                }
            }
        }

        private void PopulateList()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).TryGetComponent(out ObjectLocator tempLocator))
                {
                    objectsToLocate.Add(tempLocator); 
                }
            }
        }
    }
}