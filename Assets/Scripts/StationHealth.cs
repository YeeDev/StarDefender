using System;
using UnityEngine;

public class StationHealth : MonoBehaviour
{
    public event Action OnTakeDamage;

    [SerializeField] [Range(0, 100)] int maximumHealthPoints = 10;

    public int GetHealthPoints { get => maximumHealthPoints; }

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
        if (OnTakeDamage != null) { OnTakeDamage(); }
    }
}
