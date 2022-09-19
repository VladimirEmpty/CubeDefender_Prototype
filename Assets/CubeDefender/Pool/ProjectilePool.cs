using UnityEngine;

namespace CubeDefender.Pool
{
    public sealed class ProjectilePool : BaseAllocatedGameObjectPool<Rigidbody>
    {
        public ProjectilePool(Rigidbody prototype) : base(prototype)
        {
        }

        public Rigidbody Spawn(Vector3 position)
        {
            var projectile = base.Spawn();
            projectile.transform.position = position;

            return projectile;
        }

        protected override void OnDespawn(Rigidbody despawnedObject)
        {
            despawnedObject.velocity = Vector3.zero;
            despawnedObject.angularVelocity = Vector3.zero;

            despawnedObject.gameObject.SetActive(false);
        }

        protected override void OnSpawn(Rigidbody spawnedObject)
        {
            spawnedObject.gameObject.SetActive(true);
        }
    }
}
