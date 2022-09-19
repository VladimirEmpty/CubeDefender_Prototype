using UnityEngine;
using TMPro;

using CubeDefender.GUI.MVC.View;

namespace CubeDefender.GUI.Screen.PlayerInfo
{
    public sealed class PlayerInfoScreen : MonoBehaviour, IView
    {
        [SerializeField] private TextMeshProUGUI _playerScore;

        public TextMeshProUGUI PlayerScore => _playerScore;
    }
}
