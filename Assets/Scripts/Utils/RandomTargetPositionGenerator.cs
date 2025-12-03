using UnityEngine;

namespace Utils
{
    public class RandomTargetPositionGenerator
    {
        private readonly Transform _generationCenter;
        private readonly float _generationRadius;

        public RandomTargetPositionGenerator(Transform generationCenter, float generationRadius)
        {
            _generationCenter = generationCenter;
            _generationRadius = generationRadius;
        }

        public Vector3 GetTargetPosition()
        {
            Vector2 randomCirclePoint = _generationRadius * Random.insideUnitCircle;
            return new Vector3(randomCirclePoint.x, 0, randomCirclePoint.y) + _generationCenter.position;
        }
    }
}
