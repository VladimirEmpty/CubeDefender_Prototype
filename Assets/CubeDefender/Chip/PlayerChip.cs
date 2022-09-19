using UnityEngine;

namespace CubeDefender.Chip
{
    public sealed class PlayerChip : BaseChip
    {
        [SerializeField] private Transform _firePoint;

        public Transform FirePoint => _firePoint;
    }
}
