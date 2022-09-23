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

        bool weakPointDestroyed;
        HealthStat health;

        public bool IsDestroyed { get => weakPointDestroyed; }

        private void Awake() { health = FindObjectOfType<HealthStat>(); }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Missile"))
            {
                weakPointDestroyed = true;
                Destroy(other.gameObject);
                audioSource.Play();
                explosionParticles.Play();
                health.TakeDamage(generatorAttached);
            }
        }
    }
}