using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 10f)] float spawnTime = 1f;
    [SerializeField] [Range(0f, 50f)] int poolSize = 5;
    [SerializeField] GameObject enemyPrefab = null;

    private void Awake()
    {
        PopulatePool();
    }

    private void PopulatePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            Instantiate(enemyPrefab, transform);
        }
    }
}
