using UnityEngine;

namespace Props
{
    public class BombView  : MonoBehaviour
    {
        [SerializeField] private Material[] _flickerMaterials;
        [SerializeField] private float _flickInterval;
        [SerializeField] private ParticleSystem _explosionParticlesPrefab;
        [SerializeField] private Bomb _bomb;

        private Flicker _flicker;
        private ParticleSystem _explosionParticles;

        private void Awake()
        {
            Renderer meshRenderer = GetComponent<Renderer>();
            _flicker = new Flicker(meshRenderer, _flickerMaterials, _flickInterval);
        }

        private void Update()
        {
            if(_bomb.IsActive && _flicker.IsActive == false)
                _flicker.Start();

            if (_bomb.IsActive == false && _flicker.IsActive)
                _flicker.Stop();

            if(_bomb.IsExplode && _explosionParticles == null)
                Explode();

            _flicker.Update();
        }

        private void Explode()
        {
            _explosionParticles = Instantiate(_explosionParticlesPrefab, transform.position, Quaternion.identity);
        }
    }
}
