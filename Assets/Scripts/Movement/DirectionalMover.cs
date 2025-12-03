using UnityEngine;

namespace Movement
{
    public class DirectionalMover
    {
        private readonly CharacterController _characterController;
        private readonly float _movementSpeed;

        private Vector3 _currentDirection;

        public DirectionalMover(CharacterController characterController, float movementSpeed)
        {
            _characterController = characterController;
            _movementSpeed = movementSpeed;
        }

        public Vector3 CurrentVelocity { get; private set; }

        public void SetInputDirection(Vector3 direction) => _currentDirection = direction;

        public void Update(float deltaTime)
        {
            CurrentVelocity = _currentDirection.normalized * _movementSpeed;
            _characterController.Move(CurrentVelocity * deltaTime);
        }
    }
}
