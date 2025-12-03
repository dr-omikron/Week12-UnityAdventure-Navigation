using PlayerInputs;
using UnityEngine;

namespace Spawners
{
    public class MovementPointIndicatorSpawner
    {
        private readonly MovementPointIndicator _movementPointIndicatorPrefab;
        private MovementPointIndicator _currentMovementPointIndicator;

        private readonly float _movementPointYOffset;

        public MovementPointIndicatorSpawner(MovementPointIndicator movementPointIndicatorPrefab, float movementPointYOffset)
        {
            _movementPointIndicatorPrefab = movementPointIndicatorPrefab;
            _movementPointYOffset = movementPointYOffset;
        }

        public void CreateMovementPointIndicator(Vector3 position)
        {
            _currentMovementPointIndicator = Object.Instantiate(_movementPointIndicatorPrefab,
                new Vector3(position.x, position.y + _movementPointYOffset, position.z),
                Quaternion.identity);
        }

        public void DestroyCurrentIndicator()
        {
            Object.Destroy(_currentMovementPointIndicator.gameObject);
            _currentMovementPointIndicator = null;
        }

        public bool IsAlreadyIndicatorExist() => _currentMovementPointIndicator != null;
    }
}
