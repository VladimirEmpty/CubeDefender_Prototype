using CubeDefender.Chip;
using CubeDefender.GameSettings;

namespace CubeDefender.Pool
{
    public sealed class EnemyChipPool : BasePoolComplex<EnemyChipType, EnemyChip>
    {
        public EnemyChipPool(GameResourcesSetting gameResourcesSetting) : base(gameResourcesSetting)
        {
        }

        protected override EnemyChip GetPrefab(EnemyChipType key)
        {
            return GameResourcesSetting.GetEnemyChipPrefab(key);
        }
    }
}
