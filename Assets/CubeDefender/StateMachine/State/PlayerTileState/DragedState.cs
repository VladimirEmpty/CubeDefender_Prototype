using CubeDefender.GameFieldTile;

namespace CubeDefender.StateMachine.State.PlayerTileState
{
    public sealed class DragedState : BaseState<PlayerTile>
    {
        public override bool IsUpdatable => false;

        public override void Enter()
        {
            StateOwner.View.TileMeshRenderer.material = StateOwner.View.PlayerTileViewSetting.DragedMaterial;
        }

        public override void Exit()
        {
            StateOwner.View.TileMeshRenderer.material = StateOwner.View.PlayerTileViewSetting.DeselectedMaterial;
        }
    }
}
