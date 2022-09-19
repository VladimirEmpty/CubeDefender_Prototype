using UnityEngine;

using CubeDefender.Locator;
using CubeDefender.Service;
using CubeDefender.Chip;

namespace CubeDefender
{
    /// <summary>
    /// Класс-помощник по обработки физических взаимодействий ировых объектов на поле
    /// </summary>
    public static class GameCollisionHandler
    {
        public static void ExecuteCollision(this EnemyChip enemyChip, Collider other)
        {
            var enemyChipService = ServiceLocator.Get<EnemyChipService>();
            var projectileService = ServiceLocator.Get<ProjectileService>();

            enemyChipService.ExecuteHit(enemyChip, projectileService.GetProjectileDamage(other.gameObject.GetHashCode()));
            projectileService.Remove(other.gameObject.GetHashCode());
        }

        public static void ExecuteCollision(this DeadZoneChip deadZone, Collider other)
        {
            var projectileService = ServiceLocator.Get<ProjectileService>();
            projectileService.Remove(other.gameObject.GetHashCode());
        }
    }
}
