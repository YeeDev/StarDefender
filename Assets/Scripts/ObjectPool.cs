using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] [Range(0f, 50f)] int poolSize = 5;
    [SerializeField] GameObject enemyPrefab = null;
    [SerializeField] bool createsMoreEnemies = true;

    private void Awake()
    {
        PopulatePool();
    }

    private void PopulatePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            CreateNewEnemy();
        }
    }

    private EnemyMover CreateNewEnemy()
    {
        EnemyMover enemy = Instantiate(enemyPrefab, transform).GetComponent<EnemyMover>();
        enemy.gameObject.SetActive(false);

        return enemy;
    }

    public EnemyMover GetEnemy()
    {
        foreach (Transform enemy in transform)
        {
            if (!enemy.gameObject.activeInHierarchy) { return enemy.GetComponent<EnemyMover>(); }
        }

        if (createsMoreEnemies) { return CreateNewEnemy(); }

        return null;
    }
}
