using UnityEngine;
using UnityEngine.UI;

using CubeDefender.GUI.MVC.View;

namespace CubeDefender.GUI.Popup.RestartGame
{
    public sealed class RestartGamePopup : MonoBehaviour, IView
    {
        [SerializeField] private Button _restartButton;

        public Button RestartButton => _restartButton;
    }
}
