using UnityEngine;

namespace CubeDefender.Chip
{
    [RequireComponent(typeof(BoxCollider))]
    public sealed class DeadZoneChip : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            GameCollisionHandler.ExecuteCollision(this, other);
        }
    }
}
