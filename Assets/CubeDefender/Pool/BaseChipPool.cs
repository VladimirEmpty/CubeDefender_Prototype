using UnityEngine;

using CubeDefender.Chip;

namespace CubeDefender.Pool
{
    public abstract class BaseChipPool<T> : BaseAllocatedGameObjectPool<T>
        where T : BaseChip
    {
        public BaseChipPool(T prototype) : base(prototype)
        {
        }

        public T Spawn(Vector3 position)
        {
            var chip = base.Spawn();
            chip.transform.position = position;

            return chip;
        }

        protected override void OnDespawn(T despawnedObject)
        {
            despawnedObject.gameObject.SetActive(false);
        }

        protected override void OnSpawn(T spawnedObject)
        {
            spawnedObject.gameObject.SetActive(true);
        }
    }
}
