using UnityEngine;

namespace Props
{
    public class ExplodableActivator : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {

            IExplodable explodable = other.GetComponent<IExplodable>();
            
            if (explodable != null)
                explodable.Activate();
        }

        private void OnTriggerExit(Collider other)
        {
            IExplodable explodable = other.GetComponent<IExplodable>();

            if (explodable != null)
                explodable.Deactivate();
        }
    }
}
