using CubeDefender.Locator;
using CubeDefender.Chip;
using CubeDefender.GameFieldTile;
using CubeDefender.StateMachine;
using CubeDefender.StateMachine.State.PlayerTileState;

using CubeDefender.GUI.MVC;
using CubeDefender.GUI.Screen.PlayerMainButton;


namespace CubeDefender.Service
{
    public sealed class PlayerInputService : IService
    {
        private readonly PlayerChipService PlayerChipService;

        public PlayerInputService()
        {
            PlayerChipService = ServiceLocator.Get<PlayerChipService>();
        }

        public PlayerTile StartDragTile { get; private set; }
        public PlayerTile EndDragTile { get; private set; }
        public bool IsOnDragPlayerChip { get; private set; }

        public void PlayerStartDragChip(PlayerTile tile)
        {
            IsOnDragPlayerChip = true;

            if (tile == null) return;
            if (!PlayerChipService.ContainChip(tile.PosX, tile.PosY))
            {
                IsOnDragPlayerChip = false;
                return;
            }

            PlayerChipService.HideChip(
               tile.PosX,
               tile.PosY);

            ConectorMVC.UpdateController<PlayerMainButtonController>();

            tile.ChangeState<PlayerTile, DragedState>();
            StartDragTile = tile;
        }

        public void PlayerEndDragChip()
        {
            IsOnDragPlayerChip = false;

            Reset();

            if (EndDragTile == null) return;

            if(StartDragTile == null)
            {
                PlayerChipService.ExecuteAddChip(
                                    EndDragTile.PosX,
                                    EndDragTile.PosY,
                                    PlayerChipRank.I);
            }
            else
            {
                PlayerChipService.ExecuteMoveChip(
                                    ConvertTo(StartDragTile),
                                    ConvertTo(EndDragTile));
            }

            EndDragTile.Reset();

            StartDragTile = null;
            EndDragTile = null;
        }

        public void PlayerOnEnterTile(PlayerTile tile)
        {
            if (!IsOnDragPlayerChip) return;
            if (StartDragTile != null && StartDragTile.Equals(tile)) return;

            tile.ChangeState<PlayerTile, SelectedState>();
            EndDragTile = tile;
        }

        public void PlayerOnExiteTile(PlayerTile tile)
        {
            if (!IsOnDragPlayerChip) return;
            if (StartDragTile != null && StartDragTile.Equals(tile)) return;
                
            tile.Reset();
            EndDragTile = null;
        }

        private void Reset()
        {
            ConectorMVC.UpdateController<PlayerMainButtonController>();

            if (StartDragTile == null) return;

            StartDragTile.Reset();

            PlayerChipService.ShowChip(
                StartDragTile.PosX,
                StartDragTile.PosY);
        }

        private (int x, int y) ConvertTo(PlayerTile tile)
        {
            return (tile.PosX, tile.PosY);
        }
    }
}
