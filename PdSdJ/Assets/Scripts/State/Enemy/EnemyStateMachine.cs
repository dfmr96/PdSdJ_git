using System;

namespace State.Enemy
{
    [Serializable]
    public class EnemyStateMachine : StateMachine
    {
        public global::Enemy Enemy { get; private set; }
        public IdleState IdleState { get; }
        public ChaseState ChaseState { get; }

        public EnemyStateMachine(global::Enemy enemy)
        {
            Enemy = enemy;
            IdleState = new IdleState(this);
            ChaseState = new ChaseState(this);
            Initialize(IdleState);
        }
    }
}