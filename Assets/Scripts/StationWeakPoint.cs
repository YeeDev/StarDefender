using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationWeakPoint : MonoBehaviour
{
    Animator animator;
    ParticleSystem explosion;
    AudioSource audioSource;
    StationHealth stationHealth;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        explosion = GetComponentInChildren<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        stationHealth = FindObjectOfType<StationHealth>();
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
        stationHealth.TakeDamage();

        PlaySpecialEffects();
    }

    private void PlaySpecialEffects()
    {
        explosion.Play();
        animator.SetTrigger("TakeDamage");
        audioSource.Play();
    }
}
