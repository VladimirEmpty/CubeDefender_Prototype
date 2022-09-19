using CubeDefender.Locator;
using CubeDefender.Service;

namespace CubeDefender.Builder
{
    public struct GameRestartBuilder : IBuilder
    {
        public void Build()
        {
            ServiceLocator.Get<PlayerChipService>().Clear();
            ServiceLocator.Get<EnemyChipService>().Clear();
            ServiceLocator.Get<PlayerService>().Clear();
        }
    }
}
