using Movement;
using PlayerInputs;
using UnityEngine;
using Utils;

namespace Spawners
{
    public class MovementPointIndicatorSpawner : MonoBehaviour
    {
        private IDirectionalMovable _directionalMovable;
        private MovementPointIndicator _movementPointIndicatorPrefab;

        private MovementPointIndicator _currentMovementPointIndicator;

        public void Initialize(IDirectionalMovable directionalMovable, MovementPointIndicator movementPointIndicatorPrefab)
        {
            _directionalMovable = directionalMovable;
            _movementPointIndicatorPrefab = movementPointIndicatorPrefab;
        }

        private void Update()
        {
            if (IsTargetReached() == false && IsAlreadyIndicatorExist() == false)
            {
                CreateMovementPointIndicator(_directionalMovable.TargetPosition);
            }

            if(IsAlreadyIndicatorExist())
                MoveIndicatorToTarget(_directionalMovable.TargetPosition);

            if(IsTargetReached() && IsAlreadyIndicatorExist())
                DestroyCurrentIndicator();
        }

        private void CreateMovementPointIndicator(Vector3 position)
        {
            _currentMovementPointIndicator = Instantiate(_movementPointIndicatorPrefab,
                new Vector3(position.x, position.y + Constants.MovementPointIndicatorYOffset, position.z),
                Quaternion.identity);
        }

        private void MoveIndicatorToTarget(Vector3 targetPosition)
        {
            _currentMovementPointIndicator.transform.position = targetPosition;

        }

        private void DestroyCurrentIndicator()
        {
            Destroy(_currentMovementPointIndicator.gameObject);
            _currentMovementPointIndicator = null;
        }

        private bool IsAlreadyIndicatorExist() => _currentMovementPointIndicator != null;
        private bool IsTargetReached() => _directionalMovable.DistanceToTarget <= Constants.MinDistanceToDestroyMovementIndicator;
    }
}
