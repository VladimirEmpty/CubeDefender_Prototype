using CubeDefender.Locator;
using CubeDefender.Service;

namespace CubeDefender.StateMachine.State.GameProcessorState
{
    public sealed class PlayerShootState : BaseState<GameProcessor>
    {
        private readonly PlayerChipService PlayerChipService;
        private readonly ProjectileService ProjectileService;

        public PlayerShootState()
        {
            PlayerChipService = ServiceLocator.Get<PlayerChipService>();
            ProjectileService = ServiceLocator.Get<ProjectileService>();
        }

        public override bool IsUpdatable => true;

        public override void Enter()
        {
            PlayerChipService.ExecuteFireChip();
        }

        public override void Update()
        {
            if (ProjectileService.MovementProjectileCount > 0) return;

            StateOwner.ChangeState<EnemyMovementState>(state => state.StateOwner = this.StateOwner);
        }
    }
}
