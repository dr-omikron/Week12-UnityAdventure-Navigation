using UnityEngine;

namespace Movement
{
    public interface IMovementTarget
    {
        float DistanceToTarget { get; }
        Vector3 TargetPosition { get; }
        void SetDistanceToTarget(float distanceToTarget);
        void SetTargetPosition(Vector3 targetPosition);
    }
}
