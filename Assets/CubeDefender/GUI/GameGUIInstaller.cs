using UnityEngine;

using CubeDefender.GUI.MVC;
using CubeDefender.GUI.Screen.PlayerMainButton;
using CubeDefender.GUI.Screen.PlayerInfo;
using CubeDefender.GUI.Popup.RestartGame;

namespace CubeDefender.GUI
{
    public sealed class GameGUIInstaller : MonoBehaviour
    {
        [SerializeField] private PlayerMainButtonScreen _playerMainButtonScreen;
        [SerializeField] private PlayerInfoScreen _playerInfoScreen;
        [SerializeField] private RestartGamePopup _restartGamePopup;

        private void Start()
        {
            _playerMainButtonScreen.AddController<PlayerMainButtonController>();
            _playerInfoScreen.AddController<PlayerInfoController>();
            _restartGamePopup.AddController<RestartGameController>();
        }
    }
}
