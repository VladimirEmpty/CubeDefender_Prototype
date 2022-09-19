using CubeDefender.Chip;
using CubeDefender.GameSettings;

namespace CubeDefender.Pool
{
    public sealed class PlayerChipPool : BasePoolComplex<PlayerChipRank, PlayerChip>
    {
        public PlayerChipPool(GameResourcesSetting gameResourcesSetting) : base(gameResourcesSetting)
        {
        }

        protected override PlayerChip GetPrefab(PlayerChipRank key)
        {
            return GameResourcesSetting.GetPlayerChipPrefab(key);
        }
    }
}
