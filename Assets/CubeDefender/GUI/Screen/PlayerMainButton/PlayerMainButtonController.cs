using CubeDefender.GUI.MVC.Controller;

namespace CubeDefender.GUI.Screen.PlayerMainButton
{
    public sealed class PlayerMainButtonController : BaseUpdatableController<PlayerMainButtonScreen, PlayerMainButtonModel>
    {
        public override string Tag => nameof(PlayerMainButtonController);

        public override void UpdateView()
        {
            Model.Request();
            ChangeCreateButtonGroupVisual(Model.IsShowedPanel);
        }

        protected override void OnShow()
        {
            LinkedView.CreatePlayerChipElement.OnPlayerStartDrag += CreatePlayerChipOnStartDragHandling;
            LinkedView.CreatePlayerChipElement.OnPlayerEndDrag += CreatePlayerChipOnEndDragHandling;

            UpdateView();
        }

        private void CreatePlayerChipOnStartDragHandling()
        {
            Model.selectedButtonActionType = SelectedButtonActionType.CreateChipDrag;
            Model.Update();

            ChangeCreateButtonGroupVisual(false);
        }

        private void CreatePlayerChipOnEndDragHandling()
        {
            Model.selectedButtonActionType = SelectedButtonActionType.CreateChipDrop;
            Model.Update();
        }

        private void ChangeCreateButtonGroupVisual(bool status)
        {
            LinkedView.ButtonCanvasGroup.alpha = status ? 1f : 0f;
            LinkedView.ButtonCanvasGroup.interactable = status;

            LinkedView.CreatePlayerChipElement.CanvasGroup.blocksRaycasts = Model.IsEnableKey;
            LinkedView.CreatePlayerChipElement.Image.color = Model.IsEnableKey
                                                                    ? LinkedView.CreatePlayerChipElement.EnableColor
                                                                    : LinkedView.CreatePlayerChipElement.DisableColor;
        }
    }
}
