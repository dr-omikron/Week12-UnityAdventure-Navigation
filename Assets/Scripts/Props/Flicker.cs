using Timers;
using UnityEngine;

namespace Props
{
    public class Flicker
    {
        private readonly Renderer _targetRenderer;
        private readonly Material[] _targetMaterials;
        private readonly Material[] _flickerMaterials;

        private readonly Timer _flickerTimer;
        private bool _isActive;
        private bool _isFlickOn;

        public Flicker(Renderer targetRenderer, Material[] flickerMaterial, float flickerInterval)
        {
            _targetRenderer = targetRenderer;
            _flickerMaterials = flickerMaterial;

            _targetMaterials = _targetRenderer.materials;
            _flickerTimer = new Timer(flickerInterval);
        }

        public void Update()
        {
            if (_isActive == false)
                return;

            if (_flickerTimer.TimeIsUp)
                SwitchMaterial();

            _flickerTimer.Update(Time.deltaTime);
        }

        public void Start()
        {
            _flickerTimer.Start();
            _isActive = true;
        }

        public void Stop()
        {
            _flickerTimer.Stop();
            _isActive = false;

            if(_isFlickOn)
                SwitchMaterial();
        }

        private void SwitchMaterial()
        {
            _isFlickOn = !_isFlickOn;
            _targetRenderer.materials = _isFlickOn ? _flickerMaterials : _targetMaterials;
        }
    }
}
