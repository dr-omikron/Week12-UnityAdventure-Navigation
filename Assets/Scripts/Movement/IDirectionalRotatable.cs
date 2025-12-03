using UnityEngine;

namespace Movement
{
    public interface IDirectionalRotatable : ITransformPosition
    {
        Quaternion CurrentRotation { get; }

        public void SetRotationDirection(Vector3 direction);
    }
}
