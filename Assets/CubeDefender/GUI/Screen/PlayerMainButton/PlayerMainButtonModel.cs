using CubeDefender.GUI.MVC.Model;
using CubeDefender.Service;
using CubeDefender.Locator;

namespace CubeDefender.GUI.Screen.PlayerMainButton
{
    public enum SelectedButtonActionType
    {
        None = 0,
        CreateChipDrag,
        CreateChipDrop,
        UseBomb,
        UseFreez
    }

    public sealed class PlayerMainButtonModel : IModel
    {
        private readonly PlayerInputService PlayerInputService;
        private readonly PlayerService PlayerService;

        public SelectedButtonActionType selectedButtonActionType = SelectedButtonActionType.None;

        public PlayerMainButtonModel()
        {
            PlayerInputService = ServiceLocator.Get<PlayerInputService>();
            PlayerService = ServiceLocator.Get<PlayerService>();
        }

        public bool IsShowedPanel { get; private set; }
        public bool IsEnableKey { get; private set; }
        public bool IsCanCreatePlayerChip { get; private set; }
        

        public void Request()
        {
            IsShowedPanel = !PlayerInputService.IsOnDragPlayerChip;
            IsEnableKey = PlayerService.IsContainKey;
        }

        public void Update()
        {
            switch (selectedButtonActionType)
            {
                case SelectedButtonActionType.CreateChipDrag:
                    PlayerInputService.PlayerStartDragChip(null);
                    break;
                case SelectedButtonActionType.CreateChipDrop:
                    PlayerInputService.PlayerEndDragChip();
                    break;
                case SelectedButtonActionType.UseBomb:
                    break;
                case SelectedButtonActionType.UseFreez:
                    break;
            }

            selectedButtonActionType = SelectedButtonActionType.None;
            IsShowedPanel = false;
        }
    }
}
