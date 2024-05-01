using UnityEngine.AI;

namespace State.Enemy
{
    public class IdleState : StateBase
    {
        private readonly EnemyStateMachine _enemyStateMachine;
        private readonly global::Enemy _enemy;
        private readonly PlayerDetector _playerDetector;
        public IdleState(EnemyStateMachine enemyStateMachine)
        {
            _enemyStateMachine = enemyStateMachine;
            _enemy = _enemyStateMachine.Enemy;
            _playerDetector = _enemy.PlayerDetector;
        }
        public override void Enter()
        {
            _playerDetector.OnPlayerDetected += PlayerDetected;
            _enemy.Agent.isStopped = true;
        }

        public override void Update()
        {
            //
        }

        public override void Exit()
        {
            _enemy.Agent.isStopped = false;
            _playerDetector.OnPlayerDetected -= PlayerDetected;
        }

        public void PlayerDetected(CharacterController characterController)
        {
            if (characterController != null)
            {
                _enemyStateMachine.ChangeStateTo(_enemyStateMachine.ChaseState);
            }
        }
    }
}