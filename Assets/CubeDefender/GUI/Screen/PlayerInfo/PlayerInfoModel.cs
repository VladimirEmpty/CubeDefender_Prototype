using CubeDefender.GUI.MVC.Model;
using CubeDefender.Locator;
using CubeDefender.Service;

namespace CubeDefender.GUI.Screen.PlayerInfo
{
    public sealed class PlayerInfoModel : IModel
    {
        private readonly PlayerService PlayerService;

        public PlayerInfoModel()
        {
            PlayerService = ServiceLocator.Get<PlayerService>();
        }

        public int PlayerScore { get; private set; }

        public void Request()
        {
            PlayerScore = PlayerService.PlayerScore;
        }

        public void Update()
        {            
        }
    }
}
