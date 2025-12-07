using UnityEngine;

namespace Movement
{
    public interface IDirectionalMovable : ITransformPosition, IMovementTarget
    {
        Vector3 CurrentVelocity { get; }

        void SetMoveDirection(Vector3 direction);
    }
}
