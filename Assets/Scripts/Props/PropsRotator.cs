using UnityEngine;

namespace Props
{
    public class PropsRotator : MonoBehaviour
    {
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private float _frequency;
        [SerializeField] private float _amplitude;

        private float _time;
        private Vector3 _startPosition;

        private void Awake()
        {
            _startPosition = transform.position;
        }

        private void Update()
        {
            transform.Rotate(Vector3.up, Time.deltaTime * _rotateSpeed);

            _time += Time.deltaTime * _frequency;
            transform.position = _startPosition + Vector3.up * Mathf.Sin(_time) / _amplitude;
        }
    }
}
