using System;
using ScriptableObjects.Enemies;
using UnityEngine;

namespace Factory
{
    public class EnemyLocator : ObjectLocator
    {
        [field: SerializeField] public EnemyData Enemy { get; private set;}

        private void Awake()
        {
            ObjectName = Enemy.Name;
        }
    }
}