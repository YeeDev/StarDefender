using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarDef.Paths;
using StarDef.Enemies;
using StarDef.Info;
using StarDef.Tiles;

namespace StarDef.GameSequences
{
    [CreateAssetMenu(fileName = "New Wave", menuName = "Sequences/Wave")]
    public class WaveSO : ScriptableObject, ISequence
    {
        [SerializeField] int numberOfEnemies = 5;
        [SerializeField] float timeBetweenEnemies = 1f;
        [SerializeField] GameObject enemyType = null;
        [SerializeField] Vector2Int startPoint, endPoint;
        [SerializeField] Vector2Int[] alternativeEndPoints = null;
        [SerializeField] ScriptableObject beforeWaveDecorator = null;
        [SerializeField] ScriptableObject middleWaveDecorator = null;
        [SerializeField] ScriptableObject endWaveDecorator = null;

        public IEnumerator PlaySequence(SequenceVariableHolder infoHolder) { yield return PlayWave(infoHolder); }

        public IEnumerator PlayWave(SequenceVariableHolder infoHolder)
        {
            if (beforeWaveDecorator != null)
            {
                ISequence decorator = (ISequence)beforeWaveDecorator;
                yield return decorator.PlaySequence(infoHolder);
            }

            int totalEnemiesSpawned = 0;

            while (totalEnemiesSpawned < numberOfEnemies)
            {
                List<Transform> path = infoHolder.GetPathFinder.FindPath(startPoint, GetEndPoint(infoHolder));

                totalEnemiesSpawned++;
                Ship enemy = Instantiate(enemyType, CalculateSpawnPoint(path[0]), Quaternion.identity).GetComponent<Ship>();
                enemy.SetPath = path;
                enemy.SetInfoHolder = infoHolder;
                infoHolder.AddActiveEnemy();

                yield return new WaitForSeconds(timeBetweenEnemies);

                if (middleWaveDecorator != null)
                {
                    ISequence decorator = (ISequence)middleWaveDecorator;
                    yield return decorator.PlaySequence(infoHolder);
                }
            }

            if (endWaveDecorator != null)
            {
                ISequence decorator = (ISequence)endWaveDecorator;
                yield return decorator.PlaySequence(infoHolder);
            }
        }

        private Vector2Int GetEndPoint(SequenceVariableHolder infoHolder)
        {
            if (infoHolder.GetPathFinder.WeakPointDestroyed(endPoint))
            {
                foreach (Vector2Int otherEndPoint in alternativeEndPoints)
                {
                    if (!infoHolder.GetPathFinder.WeakPointDestroyed(otherEndPoint)) { return otherEndPoint; }
                }
            }

            return endPoint;
        }

        private Vector3 CalculateSpawnPoint(Transform initialTile)
        {
            Vector3 spawnVector3 = initialTile.position;
            spawnVector3.y = enemyType.transform.position.y;
            
            return spawnVector3;
        }
    }
}