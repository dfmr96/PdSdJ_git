namespace State
{
    public interface IState
    {
        public string StateName { get; }
        public void Enter();
        public void Update();
        public void Exit();
    }
}