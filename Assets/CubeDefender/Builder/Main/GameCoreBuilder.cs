using System;

using CubeDefender.Locator;
using CubeDefender.Service;
using CubeDefender.GameSettings;

namespace CubeDefender.Builder
{
    public struct GameCoreBuilder : IBuilder
    {
        private GameCommonSetting _gameCommonSetting;

        public GameCoreBuilder WhereGameCommonSetting(GameCommonSetting gameCommonSetting)
        {
            _gameCommonSetting = gameCommonSetting;

            return this;
        }

        public void Build()
        {
            if (_gameCommonSetting == null)
                throw new NullReferenceException("[SetupGameBuilder] Not found GameCoomonSetting implelentation!");

            var playerService = new PlayerService(_gameCommonSetting);
            ServiceLocator.Add(playerService);

            ServiceLocator.Add<ProjectileService>();
            ServiceLocator.Add<EnemyChipService>();
            ServiceLocator.Add<PlayerChipService>();
            ServiceLocator.Add<PlayerInputService>();
        }
    }
}
