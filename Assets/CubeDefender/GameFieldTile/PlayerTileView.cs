using UnityEngine;

using CubeDefender.GameSettings;

namespace CubeDefender.GameFieldTile
{
    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
    public sealed class PlayerTileView : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _tileMeshRenderer;
        [SerializeField] private PlayerTileViewSetting _playerTileViewSetting;

        public MeshRenderer TileMeshRenderer => _tileMeshRenderer;
        public PlayerTileViewSetting PlayerTileViewSetting => _playerTileViewSetting;
    }
}
