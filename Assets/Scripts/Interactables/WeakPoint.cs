using System.Collections.Generic;
using UnityEngine;
using StarDef.Core;
using StarDef.Tiles;

namespace StarDef.Interactables
{
    public class WeakPoint : MonoBehaviour
    {
        [SerializeField] ParticleSystem explosionParticles = null;
        [SerializeField] AudioSource audioSource = null;
        [SerializeField] EnergyGenerator[] generatorsAttached = null;
        [SerializeField] MeshRenderer indicator = null;
        [SerializeField] Color damagedColor = Color.red;

        bool weakPointDestroyed;
        Color regularColor;
        HealthStat health;
        Queue<EnergyGenerator> generatorsQueue = new Queue<EnergyGenerator>();

        public bool IsDestroyed { get => weakPointDestroyed; }

        private void Awake()
        {
            health = FindObjectOfType<HealthStat>();
            regularColor = indicator.material.color;
            foreach (var item in generatorsAttached) { generatorsQueue.Enqueue(item); }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Missile"))
            {
                weakPointDestroyed = true;
                Destroy(other.gameObject);

                TakeDamage();
                PlayVFX();
            }
        }

        private void PlayVFX()
        {
            audioSource.Play();
            explosionParticles.Play();
            ChangeColorToDamage();

            if (generatorsQueue.Count > 0) { Invoke("ChangeColorToRegular", 1); }
        }

        private void ChangeColorToDamage() { indicator.material.color = damagedColor; }
        private void ChangeColorToRegular() { indicator.material.color = regularColor; }

        private void TakeDamage()
        {
            EnergyGenerator generatorToDestroy = generatorsQueue.Count > 0 ? generatorsQueue.Dequeue() : generatorsAttached[0];
            health.TakeDamage(generatorToDestroy);
        }
    }
}