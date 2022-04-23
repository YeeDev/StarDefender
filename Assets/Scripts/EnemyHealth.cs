using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 10;
    [SerializeField] GameObject explosionParticles = null;

    int currentHitPoints = 0;

    private void OnEnable() { currentHitPoints = maxHitPoints * 2; } //Factored by 2 to fix Particles Prewarm Bug

    private void OnParticleCollision(GameObject other)
    {
        currentHitPoints--;
        if (currentHitPoints <= 0)
        {
            Instantiate(explosionParticles, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }

    private void DeactivateShip() { gameObject.SetActive(false); }
}
