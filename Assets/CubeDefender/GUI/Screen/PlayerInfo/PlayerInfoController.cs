using CubeDefender.GUI.MVC.Controller;

namespace CubeDefender.GUI.Screen.PlayerInfo
{
    public sealed class PlayerInfoController : BaseUpdatableController<PlayerInfoScreen, PlayerInfoModel>
    {
        public override string Tag => nameof(PlayerInfoController);

        public override void UpdateView()
        {
            Model.Request();
            LinkedView.PlayerScore.text = Model.PlayerScore.ToString();
        }

        protected override void OnShow() => UpdateView();
    }
}
