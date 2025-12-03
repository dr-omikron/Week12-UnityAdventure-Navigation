using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;

        private void LateUpdate()
        {
            RotateToCamera();
        }

        private void RotateToCamera()
        {
            Vector3 direction = Camera.main.transform.forward;
            transform.rotation = Quaternion.LookRotation(direction);
        }

        public void SetHealth(float healthPercentage)
        {
            _healthBar.fillAmount = healthPercentage;
        }
    }
}
