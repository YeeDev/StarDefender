using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarDef.Tiles;

//TODO change name of script
public class WeakPoint : MonoBehaviour
{
    HealthStat health;

    private void Awake() { health = FindObjectOfType<HealthStat>(); }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Missile"))
        {
            Destroy(other.gameObject);
            health.TakeDamage();
        }
    }
}
