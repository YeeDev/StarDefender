using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 10;

    int currentHitPoints = 0;

    private void OnEnable() { currentHitPoints = maxHitPoints; }

    private void OnParticleCollision(GameObject other)
    {
        currentHitPoints--;
        if (currentHitPoints <= 0) { gameObject.SetActive(false); }
    }
}
