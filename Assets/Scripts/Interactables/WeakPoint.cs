using UnityEngine;
using StarDef.Core;
using StarDef.Tiles;

namespace StarDef.Interactables
{
    public class WeakPoint : MonoBehaviour
    {
        [SerializeField] ParticleSystem explosionParticles = null;
        [SerializeField] AudioSource audioSource = null;
        [SerializeField] EnergyGenerator generatorAttached = null;

        HealthStat health;

        private void Awake() { health = FindObjectOfType<HealthStat>(); }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Missile"))
            {
                Destroy(other.gameObject);
                audioSource.Play();
                explosionParticles.Play();
                health.TakeDamage(generatorAttached);
            }
        }
    }
}