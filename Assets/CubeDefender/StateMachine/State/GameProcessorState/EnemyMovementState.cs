using CubeDefender.Locator;
using CubeDefender.Service;

namespace CubeDefender.StateMachine.State.GameProcessorState
{
    public sealed class EnemyMovementState : BaseState<GameProcessor>
    {
        private readonly EnemyChipService EnemyChipService;

        public EnemyMovementState()
        {
            EnemyChipService = ServiceLocator.Get<EnemyChipService>();
        }

        public override bool IsUpdatable => true;

        public override void Enter()
        {
            EnemyChipService.ExecuteMove();
        }

        public override void Update()
        {
            if (EnemyChipService.OnMovementChipCount > 0) return;

            StateOwner.ChangeState<EnemyCreateState>(state => state.StateOwner = this.StateOwner);
        }
    }
}
