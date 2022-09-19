using CubeDefender.Locator;
using CubeDefender.Service;

namespace CubeDefender.StateMachine.State.GameProcessorState
{
    public sealed class GameOverState : BaseState<GameProcessor>
    {
        public override bool IsUpdatable => false;

        public override void Enter()
        {
            ServiceLocator.Get<PlayerService>().GameOver();

            StateOwner.Reset();
        }
    }
}
