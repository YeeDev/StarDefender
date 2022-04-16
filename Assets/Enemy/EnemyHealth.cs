using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 10;

    int currentHitPoints = 0;

    private void Awake() { currentHitPoints = maxHitPoints; }

    private void OnParticleCollision(GameObject other)
    {
        currentHitPoints--;
        if (currentHitPoints <= 0) { Destroy(gameObject); }
    }
}
