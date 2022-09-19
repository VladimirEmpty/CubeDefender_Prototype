using CubeDefender.GameFieldTile;

namespace CubeDefender.StateMachine.State.PlayerTileState
{
    public sealed class SelectedState : BaseState<PlayerTile>
    {
        public override bool IsUpdatable => false;

        public override void Enter()
        {
            StateOwner.View.TileMeshRenderer.material = StateOwner.View.PlayerTileViewSetting.SelectedMaterial;
        }

        public override void Exit()
        {
            StateOwner.View.TileMeshRenderer.material = StateOwner.View.PlayerTileViewSetting.DeselectedMaterial;
        }
    }
}
