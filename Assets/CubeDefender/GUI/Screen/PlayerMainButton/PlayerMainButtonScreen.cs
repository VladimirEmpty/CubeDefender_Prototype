using UnityEngine;

using CubeDefender.GUI.MVC.View;
using CubeDefender.GUI.Elements;

namespace CubeDefender.GUI.Screen.PlayerMainButton
{
    public sealed class PlayerMainButtonScreen : MonoBehaviour, IView
    {
        [SerializeField] private CanvasGroup _buttonCanvasGroup;
        [SerializeField] private CreatePlayerChipElement _createPlayerChipElement;

        public CanvasGroup ButtonCanvasGroup => _buttonCanvasGroup;
        public CreatePlayerChipElement CreatePlayerChipElement => _createPlayerChipElement;
    }
}
