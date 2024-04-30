using UnityEngine.AI;

namespace State.Enemy
{
    public class ChaseState : StateBase
    {
        private readonly EnemyStateMachine _enemyStateMachine;
        private readonly EnemyController _enemyController;
        private readonly NavMeshAgent agent;
        private readonly PlayerDetector _playerDetector;

        public ChaseState(EnemyStateMachine enemyStateMachine)
        {
            _enemyStateMachine = enemyStateMachine;
            _enemyController = _enemyStateMachine.EnemyController;
            _playerDetector = _enemyController.PlayerDetector;
            agent = _enemyController.Agent;
        }
        public override void Enter()
        {
            _playerDetector.OnPlayerDetected += PlayerEscaped;
        }

        public override void Update()
        {
            CharacterController player = _playerDetector.Player;
            if (player != null) agent.SetDestination(player.transform.position);
        }

        public override void Exit()
        {
            _playerDetector.OnPlayerDetected -= PlayerEscaped;
        }
        
        public void PlayerEscaped(CharacterController characterController)
        {
            if (characterController == null)
            {
                _enemyStateMachine.ChangeStateTo(_enemyStateMachine.IdleState);
            }
        }
    }
}