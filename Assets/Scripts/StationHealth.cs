using System;
using UnityEngine;

public class StationHealth : MonoBehaviour
{
    public event Action OnTakeDamage;

    [SerializeField] [Range(0, 100)] int maximumHealthPoints = 10;
    [SerializeField] ParticleSystem explosion = null;

    Animator animator;
    AudioSource audioSource;

    public int GetHealthPoints { get => maximumHealthPoints; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyMissile"))
        {
            Destroy(other.gameObject);
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        maximumHealthPoints--;

        PlaySpecialEffects();

        if (OnTakeDamage != null) { OnTakeDamage(); }
    }

    private void PlaySpecialEffects()
    {
        explosion.Play();
        animator.SetTrigger("TakeDamage");
        audioSource.Play();
    }
}
