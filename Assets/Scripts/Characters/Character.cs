using Health;
using Movement;
using Props;
using UnityEngine;

namespace Characters
{
    public class Character : MonoBehaviour, IDirectionalMovable, IDirectionalRotatable, IDamageable
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _maxHealth;

        private DirectionalMover _mover;
        private DirectionalRotator _rotator;
        private HealthComponent _healthComponent;

        public Vector3 CurrentVelocity => _mover.CurrentVelocity;
        public Vector3 Position => transform.position;
        public Quaternion CurrentRotation => _rotator.CurrentRotation;

        private void Awake()
        {
            _mover = new DirectionalMover(GetComponent<CharacterController>(), _movementSpeed);
            _rotator = new DirectionalRotator(transform, _rotationSpeed);
            _healthComponent = new HealthComponent(_maxHealth);
        }

        private void Update()
        {
            _mover.Update(Time.deltaTime);
            _rotator.Update(Time.deltaTime);
        }

        public void SetMoveDirection(Vector3 inputDirection) => _mover.SetInputDirection(inputDirection);

        public void SetRotationDirection(Vector3 inputDirection) => _rotator.SetInputDirection(inputDirection);

        public void TakeDamage(float damage) => _healthComponent.TakeDamage(damage);
        public bool IsDamaged() => _healthComponent.IsDamaged;
        public void ResetDamaged() => _healthComponent.ResetDamaged();
        public bool IsDead() => _healthComponent.IsDead;
        public float GetHeathPercent() => _healthComponent.HealthPercent;
    }
}
