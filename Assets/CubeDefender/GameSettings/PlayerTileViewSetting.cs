using UnityEngine;

namespace CubeDefender.GameSettings
{
    [CreateAssetMenu(fileName = nameof(PlayerTileViewSetting), menuName = "CubeDefender/Settings/PlayerTileView")]
    public sealed class PlayerTileViewSetting : ScriptableObject
    {
        [SerializeField] private Material _deselectedMaterial;
        [SerializeField] private Material _selectedMaterial;
        [SerializeField] private Material _dragedMaterial;

        public Material DeselectedMaterial => _deselectedMaterial;
        public Material SelectedMaterial => _selectedMaterial;
        public Material DragedMaterial => _dragedMaterial;
    }
}
