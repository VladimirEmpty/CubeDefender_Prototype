using CubeDefender.Builder;

namespace CubeDefender.StateMachine.State.GameProcessorState
{
    public sealed class RestartState : BaseState<GameProcessor>
    {
        public override bool IsUpdatable => false;

        public override void Enter()
        {
            new GameRestartBuilder().Build();
            new CreateChipBuilder().Build();

            StateOwner.Reset();
        }
    }
}
