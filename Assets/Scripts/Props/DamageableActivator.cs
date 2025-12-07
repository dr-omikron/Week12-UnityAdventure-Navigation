using Health;
using UnityEngine;

namespace Props
{
    public class DamageableActivator : MonoBehaviour
    {
        private IExplodable _explodable;

        public void Initialize(IExplodable explodable)
        {
            _explodable = explodable;
        }

        private void OnTriggerEnter(Collider other)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            
            if (damageable != null)
                _explodable.Activate();
        }

        private void OnTriggerExit(Collider other)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();

            if (damageable != null)
                _explodable.Deactivate();
        }
    }
}
