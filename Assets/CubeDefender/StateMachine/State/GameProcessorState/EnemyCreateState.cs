using CubeDefender.Builder;

namespace CubeDefender.StateMachine.State.GameProcessorState
{
    public sealed class EnemyCreateState : BaseState<GameProcessor>
    {
        public override bool IsUpdatable => false;

        public override void Enter()
        {
            new CreateChipBuilder()
                .WhereCreateKeyStatus(true)
                .WhereCreateBonusStatus(true)
                .Build();

            StateOwner.Reset();
        }
    }
}
