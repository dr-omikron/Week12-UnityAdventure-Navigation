using Health;
using Timers;
using UnityEngine;

namespace Props
{
    public class Bomb : MonoBehaviour, IExplodable
    {
        [SerializeField] private Material[] _flickerMaterials;
        [SerializeField] private float _flickInterval;

        [SerializeField] private float _explosionDelay;
        [SerializeField] private float _explosionRadius;
        [SerializeField] private float _explosionDamage;
        [SerializeField] private ParticleSystem _explosionParticles;

        private Flicker _flicker;
        private Timer _activateTimer;
        

        private void Awake()
        {
            Renderer meshRenderer = GetComponentInChildren<Renderer>();
            _flicker = new Flicker(meshRenderer, _flickerMaterials, _flickInterval);
            _activateTimer = new Timer(_explosionDelay);
        }

        private void Update()
        {
            if(_activateTimer.TimeIsUp)
                Explode();

            _flicker.Update();
            _activateTimer.Update(Time.deltaTime);
        }

        public void Activate()
        {
            _activateTimer.Start();
            _flicker.Start();
        }

        public void Deactivate()
        {
            _activateTimer.Stop();
            _flicker.Stop();
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

            Instantiate(_explosionParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _explosionRadius);
        }
    }
}
