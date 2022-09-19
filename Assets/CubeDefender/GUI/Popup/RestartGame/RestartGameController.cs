using CubeDefender.GUI.MVC.Controller;

namespace CubeDefender.GUI.Popup.RestartGame
{
    public sealed class RestartGameController : BaseUpdatableController<RestartGamePopup, RestartGameModel>
    {
        public override string Tag => nameof(RestartGameController);

        public override void UpdateView()
        {
            Model.Request();
            LinkedView.gameObject.SetActive(Model.IsShow);
        }

        protected override void OnShow()
        {
            LinkedView.RestartButton.onClick.AddListener(
                                                () =>
                                                {
                                                    Model.Update();
                                                    UpdateView();
                                                });
            UpdateView();
        }
    }
}
