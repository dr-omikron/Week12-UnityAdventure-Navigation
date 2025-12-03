using Movement;
using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace Controllers
{
    public class RandomPointDirectionalMovableController : Controller
    {
        private readonly IDirectionalMovable _movable;
        private readonly RandomTargetPositionGenerator _randomPositionGenerator;

        private readonly NavMeshPath _path;
        private readonly NavMeshQueryFilter _queryFilter;

        private readonly float _minDistanceToTarget;
        private Vector3 _targetPosition;

        public RandomPointDirectionalMovableController(
            IDirectionalMovable movable, 
            RandomTargetPositionGenerator randomPositionGenerator, 
            NavMeshPath path, 
            NavMeshQueryFilter queryFilter, 
            float minDistanceToTarget)
        {
            _movable = movable;
            _randomPositionGenerator = randomPositionGenerator;
            _path = path;
            _queryFilter = queryFilter;
            _minDistanceToTarget = minDistanceToTarget;

            SetNewTarget();
        }

        protected override void UpdateLogic(float deltaTime)
        {
            float distanceToTarget = NavMeshUtils.GetPathLength(_path);

            if (IsTargetReached(distanceToTarget))
                SetNewTarget();

            if (NavMeshUtils.TryGetPath(_movable.Position, _targetPosition, _queryFilter, _path))
            {
                if(EnoughCornersInPath(_path))
                    _movable.SetMoveDirection(_path.corners[Constants.TargetCornerIndex] - _path.corners[Constants.StartCornerIndex]);
            }
            else
            {
                SetNewTarget();
            }
        }

        private void SetNewTarget() => _targetPosition = _randomPositionGenerator.GetTargetPosition();

        private bool IsTargetReached(float distanceToTarget) => distanceToTarget <= _minDistanceToTarget;
        private bool EnoughCornersInPath(NavMeshPath path) => path.corners.Length >= Constants.MinCornersCountInPathToMove;
    }
}
