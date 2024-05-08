using Factory;
using UnityEngine;

namespace Command
{
    public class CreateEnemyCommand : IDeletableCommand
    {
        private Enemy _enemy;
        private EnemyLocator _enemyLocator;
        public CreateEnemyCommand(Enemy enemy, EnemyLocator enemyLocator)
        {
            _enemy = enemy;
            _enemyLocator = enemyLocator;
        }
        
        public void Execute()
        {
            _enemy = Object.Instantiate(_enemy, _enemyLocator.transform.position, Quaternion.identity);
        }

        public void Undo()
        {
            if (_enemy != null)
            {
                Object.Destroy(_enemy.gameObject);
            }
        }
    }
}