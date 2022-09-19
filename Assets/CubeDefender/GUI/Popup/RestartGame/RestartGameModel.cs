using CubeDefender.GUI.MVC.Model;
using CubeDefender.Locator;
using CubeDefender.Service;

namespace CubeDefender.GUI.Popup.RestartGame
{
    public sealed class RestartGameModel : IModel
    {
        private readonly PlayerService PlayerService;

        public RestartGameModel()
        {
            PlayerService = ServiceLocator.Get<PlayerService>();
        }

        public bool IsShow { get; private set; }

        public void Request()
        {
            IsShow = PlayerService.IsGameOver;
        }

        public void Update()
        {
            GameProcessor.Instance.Restart();
        }
    }
}
