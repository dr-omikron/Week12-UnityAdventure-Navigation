using Movement;
using Physic;
using PlayerInputs;
using Spawners;
using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace Controllers
{
    public class RaycastPlayerController : Controller
    {
        private readonly IDirectionalMovable _movable;
        private readonly Raycaster _raycaster;
        private readonly PlayerInput _input;

        private readonly LayerMask _movableMask;
        private readonly NavMeshPath _path;
        private readonly NavMeshQueryFilter _queryFilter;

        private readonly MovementPointIndicatorSpawner _movementPointSpawner;
        private readonly float _minDistanceToTarget;
        private Vector3 _targetPosition;

        public RaycastPlayerController(
            IDirectionalMovable movable, 
            Raycaster raycaster, 
            PlayerInput input, 
            NavMeshPath path, 
            NavMeshQueryFilter queryFilter,
            LayerMask movableMask, 
            float minDistanceToTarget, 
            MovementPointIndicatorSpawner movementPointSpawner)
        {
            _movable = movable;
            _raycaster = raycaster;
            _input = input;
            _path = path;
            _queryFilter = queryFilter;
            _movableMask = movableMask;
            _minDistanceToTarget = minDistanceToTarget;
            _movementPointSpawner = movementPointSpawner;

            _targetPosition = movable.Position;
        }

        public bool IsMovementToTarget { get; private set; }

        protected override void UpdateLogic(float deltaTime)
        {
            if (_input.IsRayCastingStart)
            {
                if (_raycaster.CastRayFromCamera(_input.MousePosition, _movableMask, out RaycastHit hit))
                {
                    _targetPosition = hit.point;

                    if(_movementPointSpawner.IsAlreadyIndicatorExist())
                        _movementPointSpawner.DestroyCurrentIndicator();

                    _movementPointSpawner.CreateMovementPointIndicator(hit.point);
                }
            }

            float distanceToTarget = 0;

            if (NavMeshUtils.TryGetPath(_movable.Position, _targetPosition, _queryFilter, _path))
            {
                distanceToTarget = NavMeshUtils.GetPathLength(_path);

                if(EnoughCornersInPath(_path) && IsTargetReached(distanceToTarget) == false)
                {
                    _movable.SetMoveDirection(_path.corners[Constants.TargetCornerIndex] -
                                              _path.corners[Constants.StartCornerIndex]);

                    IsMovementToTarget = true;
                    return;
                }
            }

            _movable.SetMoveDirection(Vector3.zero);
            IsMovementToTarget = false;

            if(IsTargetReached(distanceToTarget) && _movementPointSpawner.IsAlreadyIndicatorExist())
                _movementPointSpawner.DestroyCurrentIndicator();
        }

        private bool IsTargetReached(float distanceToTarget) => distanceToTarget <= _minDistanceToTarget;
        private bool EnoughCornersInPath(NavMeshPath path) => path.corners.Length >= Constants.MinCornersCountInPathToMove;
    }
}
