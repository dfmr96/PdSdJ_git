using UnityEngine.AI;

namespace State.Enemy
{
    public class IdleState : StateBase
    {
        private readonly EnemyStateMachine _enemyStateMachine;
        private readonly EnemyController _enemyController;
        private readonly PlayerDetector _playerDetector;
        public IdleState(EnemyStateMachine enemyStateMachine)
        {
            _enemyStateMachine = enemyStateMachine;
            _enemyController = _enemyStateMachine.EnemyController;
            _playerDetector = _enemyController.PlayerDetector;
        }
        public override void Enter()
        {
            _playerDetector.OnPlayerDetected += PlayerDetected;
            _enemyController.Agent.isStopped = true;
        }

        public override void Update()
        {
            //
        }

        public override void Exit()
        {
            _enemyController.Agent.isStopped = false;
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