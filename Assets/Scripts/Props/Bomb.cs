using Health;
using Timers;
using UnityEngine;
using Utils;

namespace Props
{
    [RequireComponent(typeof(DamageableActivator))]

    public class Bomb : MonoBehaviour, IExplodable
    {
        [SerializeField] private float _explosionDelay;
        [SerializeField] private float _explosionRadius;
        [SerializeField] private float _explosionDamage;

        private Timer _activateTimer;
        private DamageableActivator _damageableActivator;

        public bool IsActive { get ; private set; }
        public bool IsExplode { get ; private set; }

        private void Awake()
        {
            _damageableActivator = GetComponent<DamageableActivator>();
            _damageableActivator.Initialize(this);
            _activateTimer = new Timer(_explosionDelay);
        }

        private void Update()
        {
            if(_activateTimer.TimeIsUp)
                Explode();

            _activateTimer.Update(Time.deltaTime);
        }

        public void Activate()
        {
            _activateTimer.Start();
            IsActive = true;
        }

        public void Deactivate()
        {
            _activateTimer.Stop();
            IsActive = false;
        }

        public void Explode()
        {
            Collider[] targets = Physics.OverlapSphere(transform.position, _explosionRadius);

            foreach (Collider target in targets)
            {
                IDamageable damageable = target.GetComponent<IDamageable>();

                if (damageable != null)
                    damageable.TakeDamage(_explosionDamage);
            }

            IsExplode = true;
            Destroy(gameObject, Constants.ExplodableDestroyDelay);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _explosionRadius);
        }
    }
}
