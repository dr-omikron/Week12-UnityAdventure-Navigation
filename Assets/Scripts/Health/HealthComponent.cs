
using UnityEngine;

namespace Health
{
    public class HealthComponent
    {
        private readonly float _maxHealth;
        private float _health;

        public HealthComponent(float maxHealth)
        {
            _maxHealth = maxHealth;
            Revive();
        }

        public bool IsDead { get; private set; }
        public bool IsDamaged { get; private set; }
        public float HealthPercent => _health / _maxHealth;

        public void TakeDamage(float damage)
        {
            if(damage < 0)
            {
                Debug.LogError("[HealthComponent] Damage can't be negative");
                return;
            }

            _health -= damage;

            if (_health <= 0)
            {
                _health = 0;
                IsDead = true;
                return;
            }

            IsDamaged = true;
        }

        public void Revive()
        {
            _health = _maxHealth;
            IsDead = false;
        }

        public void ResetDamaged() => IsDamaged = false;
    }
}
