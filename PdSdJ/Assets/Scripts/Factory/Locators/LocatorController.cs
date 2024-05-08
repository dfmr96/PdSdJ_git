using System;
using System.Collections.Generic;
using Command;
using ScriptableObjects.Enemies;
using UnityEngine;

namespace Factory
{
    public class LocatorController : MonoBehaviour
    {
        [SerializeField] private List<ObjectLocator> objectsToLocate;
        [SerializeField] private EnemyFactory _enemyFactory;
        [SerializeField] private PlayerDetector _playerDetector;
        private CharacterController _player;

        private void Start()
        {
            _playerDetector.OnPlayerDetected += LocateObjectsInRoom;
            _playerDetector.OnPlayerDetected += EmptyRoom;
            PopulateList();
        }

        private void OnDisable()
        {
            _playerDetector.OnPlayerDetected -= LocateObjectsInRoom;
            _playerDetector.OnPlayerDetected -= EmptyRoom;
        }

        private void LocateObjectsInRoom(CharacterController player)
        {
            if (player == null) return;

            foreach (ObjectLocator objectLocator in objectsToLocate)
            {
                if (objectLocator is EnemyLocator enemyLocator)
                {
                    GenerateEnemy(enemyLocator);
                }
            }
        }

        private void GenerateEnemy(EnemyLocator enemyLocator)
        {
            Enemy newEnemy = _enemyFactory.CreateProduct(enemyLocator.Enemy.ID);
            IDeletableCommand enemyCreationCommand = new CreateEnemyCommand(newEnemy, enemyLocator);
            EventQueue.EventQueue.Instance.EnqueueCommand(enemyCreationCommand);
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

        private void EmptyRoom(CharacterController player)
        {
            if (player != null) return;
            EventQueue.EventQueue.Instance.RemoveAllCommandOfType<CreateEnemyCommand>();
        }
    }
}