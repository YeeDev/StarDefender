using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarDef.Paths;
using StarDef.Enemies;

namespace StarDef.GameSequences
{
    [CreateAssetMenu(fileName = "New Wave", menuName = "Wave")]
    public class WaveSO : ScriptableObject, ISequence
    {
        [SerializeField] int numberOfEnemies = 5;
        [SerializeField] float timeBetweenEnemies = 1f;
        [SerializeField] GameObject enemyType = null;
        [SerializeField] Vector2Int spawnPoint = Vector2Int.zero;
        [SerializeField] Vector2Int startPoint, endPoint;
        [SerializeField] ScriptableObject decoratorSequence = null; 

        public IEnumerator PlaySequence() { yield return PlayWave(); }

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

                if (decoratorSequence != null)
                {
                    ISequence decorator = (ISequence)decoratorSequence;
                    yield return decorator.PlaySequence();
                }
            }
        }

        private Vector3 CalculateSpawnPoint()
        {
            Vector3 spawnVector3 = Vector3.zero;

            spawnVector3.x = spawnPoint.x;
            spawnVector3.y = enemyType.transform.position.y;
            spawnVector3.z = spawnPoint.y;

            return spawnVector3;
        }
    }
}