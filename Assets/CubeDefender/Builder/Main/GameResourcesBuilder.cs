using System;

using CubeDefender.Locator;
using CubeDefender.GameSettings;
using CubeDefender.Service;
using CubeDefender.Pool;

namespace CubeDefender.Builder
{
    public struct GameResourcesBuilder : IBuilder
    {
        private GameResourcesSetting _gameResourcesSetting;

        public GameResourcesBuilder WhereGameResourcesSetting(GameResourcesSetting setting)
        {
            _gameResourcesSetting = setting;

            return this;
        }

        public void Build()
        {
            if(_gameResourcesSetting == null)
                throw new NullReferenceException("[SetupGameResourcesBuilder]: Not set GameResourcesSetting.");

            var poolService = new PoolService();
            poolService.Add(new ProjectilePool(_gameResourcesSetting.Projectile));
            poolService.Add(new PlayerChipPool(_gameResourcesSetting));
            poolService.Add(new EnemyChipPool(_gameResourcesSetting));

            ServiceLocator.Add(poolService);
        }
    }
}
