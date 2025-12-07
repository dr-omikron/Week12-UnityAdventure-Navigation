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
        private Vector3 _targetPosition;

        public RaycastPlayerController(
            IDirectionalMovable movable, 
            Raycaster raycaster, 
            PlayerInput input, 
            NavMeshPath path, 
            NavMeshQueryFilter queryFilter,
            LayerMask movableMask)
        {
            _movable = movable;
            _raycaster = raycaster;
            _input = input;
            _path = path;
            _queryFilter = queryFilter;
            _movableMask = movableMask;

            _targetPosition = movable.Position;
        }

        public bool IsMovementToTarget { get; private set; }

        protected override void UpdateLogic(float deltaTime)
        {
            if (_input.IsRayCastingStart)
            {
                if (_raycaster.CastRayFromCamera(_input.MousePosition, _movableMask, out RaycastHit hit))
                    _targetPosition = hit.point;
            }

            if (NavMeshUtils.TryGetPath(_movable.Position, _targetPosition, _queryFilter, _path))
            {
                float distanceToTarget = NavMeshUtils.GetPathLength(_path);

                if(EnoughCornersInPath(_path) && IsTargetReached(distanceToTarget) == false)
                {
                    _movable.SetMoveDirection(_path.corners[Constants.TargetCornerIndex] -
                                              _path.corners[Constants.StartCornerIndex]);

                    _movable.SetDistanceToTarget(distanceToTarget);
                    _movable.SetTargetPosition(_targetPosition);
                    IsMovementToTarget = true;
                    return;
                }
            }

            _movable.SetMoveDirection(Vector3.zero);
            IsMovementToTarget = false;
        }

        private bool IsTargetReached(float distanceToTarget) => distanceToTarget <= Constants.MinDistanceToTarget;
        private bool EnoughCornersInPath(NavMeshPath path) => path.corners.Length >= Constants.MinCornersCountInPathToMove;
    }
}
