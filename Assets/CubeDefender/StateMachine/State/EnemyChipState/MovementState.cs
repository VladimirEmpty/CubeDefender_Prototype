using System;
using UnityEngine;

using CubeDefender.Chip;

namespace CubeDefender.StateMachine.State.EnemyChipState
{
    public sealed class MovementState : BaseState<EnemyChip>
    {
        private const float MathOffset = 0.975f;

        public Vector3 movementPosition;
        public float movementSpeed;

        public override bool IsUpdatable => true;

        public event Action OnDecreaseCount;

        public override void Update()
        {
            if (StateOwner.transform.position.z <= movementPosition.z * MathOffset)
            {
                StateOwner.transform.position = Vector3.MoveTowards(
                                                                StateOwner.transform.position,
                                                                movementPosition,
                                                                movementSpeed * Time.deltaTime);
            }
            else
                StateOwner.Reset();
        }

        public override void Exit()
        {
            StateOwner.transform.position = movementPosition;
            OnDecreaseCount?.Invoke();
        }

        protected override void OnDispose()
        {
            OnDecreaseCount = null;
            base.OnDispose();
        }
    }
}
