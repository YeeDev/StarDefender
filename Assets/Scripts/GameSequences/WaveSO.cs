using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarDef.Paths;
using StarDef.Enemies;

namespace StarDef.GameSequences
{
    [CreateAssetMenu(fileName = "New Wave", menuName = "Wave")]
    public class WaveSO : ScriptableObject
    {
        [SerializeField] int numberOfEnemies = 5;
        [SerializeField] float timeBetweenEnemies = 1f;
        [SerializeField] GameObject enemyType = null;
        [SerializeField] Vector2Int spawnPoint = Vector2Int.zero;
        [SerializeField] Vector2Int startPoint, endPoint;

        public IEnumerator PlayWave()
        {
            int totalEnemiesSpawned = 0;
            PathFinder pathFinder = FindObjectOfType<PathFinder>();
            List<Transform> path = pathFinder.FindPath(startPoint, endPoint);

            while (totalEnemiesSpawned < numberOfEnemies)
            {
                totalEnemiesSpawned++;
                Instantiate(enemyType, CalculateSpawnPoint(), Quaternion.identity).GetComponent<Ship>().SetPath = path;
                yield return new WaitForSeconds(timeBetweenEnemies);
            }
        }

        private Vector3Int CalculateSpawnPoint()
        {
            Vector3Int spawnVector3 = Vector3Int.zero;

            spawnVector3.x = spawnPoint.x;
            spawnVector3.z = spawnPoint.y;

            return spawnVector3;
        }
    }
}