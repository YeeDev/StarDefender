using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarDef.Paths;
using StarDef.Enemies;

[CreateAssetMenu(fileName = "New Wave", menuName = "Wave")]
public class WaveSO : ScriptableObject
{
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float timeBetweenEnemies = 1f;
    [SerializeField] GameObject enemyType = null;
    [SerializeField] Vector2Int startPoint, endPoint;

    public IEnumerator PlayWave()
    {
        int totalEnemiesSpawned = 0;
        PathFinder pathFinder = FindObjectOfType<PathFinder>();
        List<Transform> path = pathFinder.FindPath(startPoint, endPoint);

        while (totalEnemiesSpawned < numberOfEnemies)
        {
            totalEnemiesSpawned++;
            Instantiate(enemyType).GetComponent<Ship>().SetPath = path;
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
    }
}