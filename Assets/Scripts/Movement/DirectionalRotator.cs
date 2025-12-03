using UnityEngine;

namespace Movement
{
    public class DirectionalRotator
    {
        private const float RotationDeadZone = 0.05f;

        private readonly Transform _transform;
        private readonly float _rotationSpeed;

        private Vector3 _currentDirection;

        public DirectionalRotator(Transform transform, float rotationSpeed)
        {
            _transform = transform;
            _rotationSpeed = rotationSpeed;
        }

        public Quaternion CurrentRotation => _transform.rotation;

        public void SetInputDirection(Vector3 direction) => _currentDirection = direction;

        public void Update(float deltaTime)
        {
            if(_currentDirection.magnitude < RotationDeadZone)
                return;
            
            Quaternion lookRotation = Quaternion.LookRotation(_currentDirection.normalized);
            float step = _rotationSpeed * deltaTime;

            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, lookRotation, step);
        }
    }
}
