using UI;
using UnityEngine;

namespace Characters
{
    public class CharacterView : MonoBehaviour
    {
        private const float MovementDeadZone = 0.05f;
        private const float InjureHealthPercent = 0.3f;
        
        private const string InjureLayerName = "Injured Layer";

        private const float MinLayerWeightValue = 0;
        private const float MaxLayerWeightValue = 1;

        private readonly int _isRunningKey = Animator.StringToHash("IsRunning");
        private readonly int _isDamaged = Animator.StringToHash("IsDamaged");
        private readonly int _isDead = Animator.StringToHash("IsDead");

        [SerializeField] private Animator _animator;
        [SerializeField] private Character _character;
        [SerializeField] private HealthBar _healthBar;

        private int _injureLayerIndex;

        private void Awake()
        {
            _injureLayerIndex = _animator.GetLayerIndex(InjureLayerName);
        }

        private void Update()
        {
            if (_character == null)
                return;

            if(_character.CurrentVelocity.magnitude > MovementDeadZone)
                StartRunning();
            else
                StopRunning();

            if (_character.GetHeathPercent() < InjureHealthPercent)
                _animator.SetLayerWeight(_injureLayerIndex, MaxLayerWeightValue);
            else
                _animator.SetLayerWeight(_injureLayerIndex, MinLayerWeightValue);

            if(_character.IsDamaged())
                TakeDamage();

            if (_character.IsDead())
                Die();

            _healthBar.SetHealth(_character.GetHeathPercent());
        }

        public void ResetDamagedFlag()
        {
            _character.ResetDamaged();
            _animator.SetBool(_isDamaged, false);
        }

        private void StartRunning() => _animator.SetBool(_isRunningKey, true);
        private void StopRunning() => _animator.SetBool(_isRunningKey, false);
        private void TakeDamage() => _animator.SetBool(_isDamaged, true);
        private void Die() => _animator.SetBool(_isDead, true);
    }
}
