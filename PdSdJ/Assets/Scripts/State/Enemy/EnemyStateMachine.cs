using System;

namespace State.Enemy
{
    [Serializable]
    public class EnemyStateMachine : StateMachine
    {
        public EnemyController EnemyController { get; private set; }
        public IdleState IdleState { get; }
        public ChaseState ChaseState { get; }

        public EnemyStateMachine(EnemyController enemyController)
        {
            EnemyController = enemyController;
            IdleState = new IdleState(this);
            ChaseState = new ChaseState(this);
            Initialize(IdleState);
        }
    }
}