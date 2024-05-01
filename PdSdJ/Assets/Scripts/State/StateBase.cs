using System;

namespace State
{
    [Serializable]
    public abstract class StateBase : IState
    {
        protected string stateName;
        public string StateName => stateName;
        public abstract void Enter();

        public abstract void Update();

        public abstract void Exit();
    }
}