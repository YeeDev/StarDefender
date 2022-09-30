using System.Collections.Generic;
using UnityEngine;
using StarDef.Core;
using StarDef.Tiles;
using Yee.VFX;

namespace StarDef.Interactables
{
    public class WeakPoint : MonoBehaviour
    {
        [SerializeField] ParticleSystem explosionParticles = null;
        [SerializeField] AudioSource audioSource = null;
        [SerializeField] EnergyGenerator[] generatorsAttached = null;
        [SerializeField] MeshRenderer indicator = null;
        [SerializeField] Color damagedColor = Color.red;

        Color regularColor;
        HealthStat health;
        CameraShaker cameraShaker;
        Queue<EnergyGenerator> generatorsQueue = new Queue<EnergyGenerator>();

        public bool IsDestroyed { get => generatorsQueue.Count <= 0; }

        private void Awake()
        {
            health = FindObjectOfType<HealthStat>();
            cameraShaker = FindObjectOfType<CameraShaker>();
            regularColor = indicator.material.color;
            foreach (var item in generatorsAttached) { generatorsQueue.Enqueue(item); }

            health.AddHealth(generatorsAttached.Length);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Missile"))
            {
                Destroy(other.gameObject);

                TakeDamage();
                PlayVFX();
            }
        }

        private void TakeDamage()
        {
            EnergyGenerator generatorToDestroy = generatorsQueue.Count > 0 ? generatorsQueue.Dequeue() : generatorsAttached[0];
            health.TakeDamage(generatorToDestroy.IsOn);
            if (generatorToDestroy.IsOn) { generatorToDestroy.DamageGenerator(); }
        }

        private void PlayVFX()
        {
            audioSource.Play();
            explosionParticles.Play();
            ChangeColorToDamage();
            ShakeCamera();

            if (generatorsQueue.Count > 0) { Invoke("ChangeColorToRegular", 1); }
        }

        private void ShakeCamera()
        {
            float duration = health.NoHealth ? Mathf.Infinity : 1f;
            float intensity = health.NoHealth ? 0.15f : 0.25f;
            StartCoroutine(cameraShaker.Shake(duration, intensity));
        }

        private void ChangeColorToDamage() { indicator.material.color = damagedColor; }
        private void ChangeColorToRegular() { indicator.material.color = regularColor; }
    }
}