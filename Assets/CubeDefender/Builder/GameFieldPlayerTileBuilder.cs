using UnityEngine;

using CubeDefender.Locator;
using CubeDefender.Service;
using CubeDefender.Factory;
using CubeDefender.GameFieldTile;

namespace CubeDefender.Builder
{
    public struct GameFieldPlayerTileBuilder : IBuilder
    {
        private PlayerTile _playerTilePrefab;

        public GameFieldPlayerTileBuilder WherePlayerTilePrefab(PlayerTile playerTilePrefab)
        {
            _playerTilePrefab = playerTilePrefab;

            return this;
        }

        public void Build()
        {
            var playerGameFieldService = ServiceLocator.Get<PlayerGameFieldService>();
            var playerInputService = ServiceLocator.Get<PlayerInputService>();
            var container = new GameObject("PlayerTileContrainer");

            using (var objectFactory = new GameObjectFactory<PlayerTile>(_playerTilePrefab))
            {

                for (var x = 0; x < playerGameFieldService.MaxX; ++x)
                {
                    for (var y = 0; y < playerGameFieldService.MaxY; ++y)
                    {
                        var playerTile = objectFactory.Create();
                        var position = playerGameFieldService.GetCellPosition(x, y);
                        position.y = default;

                        playerTile.transform.position = position;
                        playerTile.transform.SetParent(container.transform);
                        playerTile.Setup(x, y);

                        playerTile.OnStartDragTile += playerInputService.PlayerStartDragChip;
                        playerTile.OnEndDragTile += playerInputService.PlayerEndDragChip;
                        playerTile.OnEnterTile += playerInputService.PlayerOnEnterTile;
                        playerTile.OnExitTile += playerInputService.PlayerOnExiteTile;
                    }
                }
            }
        }
    }
}
