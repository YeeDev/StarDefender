using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] float spawnTime = 1f;
    [SerializeField] int poolSize = 5;
    [SerializeField] GameObject enemyPrefab = null;

    private void Awake()
    {
        PopulatePool();
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private void PopulatePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            Instantiate(enemyPrefab, transform);
            transform.GetChild(transform.childCount - 1).gameObject.SetActive(false);
        }
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            transform.Cast<Transform>().FirstOrDefault(x => !x.gameObject.activeInHierarchy)?.gameObject.SetActive(true);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
