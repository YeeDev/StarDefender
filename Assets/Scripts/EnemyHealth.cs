using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 10;
    [SerializeField] GameObject explosionParticles = null;

    int currentHitPoints = 0;

    private void OnEnable() { currentHitPoints = maxHitPoints; }

    private void OnParticleCollision(GameObject other)
    {
        currentHitPoints--;
        if (currentHitPoints <= 0)
        {
            Instantiate(explosionParticles, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
