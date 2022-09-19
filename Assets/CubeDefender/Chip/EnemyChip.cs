using UnityEngine;

using CubeDefender.StateMachine;

namespace CubeDefender.Chip
{
    [RequireComponent(typeof(BoxCollider))]
    public sealed class EnemyChip : BaseChip, IStateMachineOwner
    {
        [SerializeField] private EnemyChipType _type;
        public EnemyChipType Type => _type;

        private void OnTriggerEnter(Collider other)
        {
            GameCollisionHandler.ExecuteCollision(this, other);
        }
    }
}
