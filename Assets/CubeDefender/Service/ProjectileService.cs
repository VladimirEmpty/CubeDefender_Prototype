using System.Collections.Generic;
using UnityEngine;

using CubeDefender.Locator;
using CubeDefender.Pool;

namespace CubeDefender.Service
{
    /// <summary>
    /// Класс-сервис с логикой работы с снарядами игрока
    /// </summary>
    public sealed class ProjectileService : IService
    {
        private const float ProjectileSpeed = 20f;
        private readonly ProjectilePool ProjectilePool; 

        private IDictionary<int, Data> _projectileMap;

        public ProjectileService()
        {
            ProjectilePool = ServiceLocator.Get<PoolService>().Get<ProjectilePool>();

            _projectileMap = new Dictionary<int, Data>();
        }

        public int MovementProjectileCount => _projectileMap.Count;

        public void Create(Transform firePoint, int force)
        {
            if (firePoint == null | force == default) return;

            var projectile = ProjectilePool.Spawn(firePoint.position);
            projectile.velocity = firePoint.forward * ProjectileSpeed;

            _projectileMap.Add(
                projectile.gameObject.GetHashCode(),
                new Data(projectile, force));
        }

        public int GetProjectileDamage(int projectileHash)
        {
            if(_projectileMap.TryGetValue(projectileHash, out var data))
            {
                return data.Damage;
            }
            else
            {
                return default;
            }
        }

        public void Remove(int projectileHash)
        {
            if(_projectileMap.TryGetValue(projectileHash, out var data))
            {
                ProjectilePool.Despawn(data.Projectile);
                _projectileMap.Remove(projectileHash);
            }

        }

        public void Clear()
        {
            foreach(var el in _projectileMap)
            {
                ProjectilePool.Despawn(el.Value.Projectile);
            }

            _projectileMap.Clear();
        }

        private readonly struct Data
        {
            public readonly Rigidbody Projectile;
            public readonly int Damage;

            public Data(Rigidbody projectile, int damage)
            {
                Projectile = projectile;
                Damage = damage;
            }
        }
    }
}
