using CubeDefender.StateMachine.State;

namespace CubeDefender.StateMachine
{
    public interface IMachine
    {
        public IState CurrentState { get; }

        public void ChangeState(IState state);
        public void Update();
    }
}
