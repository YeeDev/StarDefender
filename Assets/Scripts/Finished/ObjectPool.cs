using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] [Range(0f, 50f)] int poolSize = 5;
    [SerializeField] GameObject enemyPrefab = null;
    [SerializeField] bool createsMoreEnemies = true;

    List<EnemyMover> pool = new List<EnemyMover>();
    
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
        pool.Add(enemy);

        return enemy;
    }

    public EnemyMover GetEnemy()
    {
        EnemyMover inactiveEnemy = pool.FirstOrDefault(x => !x.gameObject.activeInHierarchy);

        if (inactiveEnemy != null) { return inactiveEnemy; }

        if (createsMoreEnemies) { return CreateNewEnemy(); }

        return null;
    }
}
