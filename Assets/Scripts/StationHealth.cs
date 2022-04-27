using System;
using UnityEngine;

public class StationHealth : MonoBehaviour
{
    public event Action OnTakeDamage;

    [SerializeField] [Range(0, 100)] int maximumHealthPoints = 10;
    [SerializeField] ParticleSystem explosion = null;

    Animator animator;

    public int GetHealthPoints { get => maximumHealthPoints; }

    private void Awake() { animator = GetComponent<Animator>(); }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyMissile"))
        {
            TakeDamage(other.gameObject);
        }
    }

    private void TakeDamage(GameObject missile)
    {
        Destroy(missile);
        maximumHealthPoints--;
        explosion.Play();
        animator.SetTrigger("TakeDamage");
        if (OnTakeDamage != null) { OnTakeDamage(); }
    }
}
