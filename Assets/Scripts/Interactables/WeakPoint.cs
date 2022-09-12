using UnityEngine;

namespace StarDef.Interactables
{
    public class WeakPoint : MonoBehaviour
    {
        [SerializeField] ParticleSystem explosionParticles = null;
        [SerializeField] AudioSource audioSource = null;

        HealthStat health;

        private void Awake() { health = FindObjectOfType<HealthStat>(); }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Missile"))
            {
                Destroy(other.gameObject);
                audioSource.Play();
                explosionParticles.Play();
                health.TakeDamage();
            }
        }
    }
}