using System;
using UnityEngine;

public class StationHealth : MonoBehaviour
{
    public event Action OnTakeDamage;

    [SerializeField] ParticleSystem explosion = null;

    int healthPoints = 5;
    Animator animator;
    AudioSource audioSource;

    public int GetHealthPoints { get => healthPoints; }

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
        healthPoints--;

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
