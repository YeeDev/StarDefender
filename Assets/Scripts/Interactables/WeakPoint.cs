using UnityEngine;

namespace StarDef.Interactables
{
    public class WeakPoint : MonoBehaviour
    {
        HealthStat health;

        private void Awake() { health = FindObjectOfType<HealthStat>(); }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Missile"))
            {
                Destroy(other.gameObject);
                health.TakeDamage();
            }
        }
    }
}