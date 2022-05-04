using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Wave Sequence", menuName = "Wave sequence")]
public class WaveSequence : ScriptableObject, IGameSequence
{
    [SerializeField] Wave wave = null;

    public IEnumerator PlaySequence(DialoguePrinter printer, Text text)
    {
        PathsHolder pathsHolder = FindObjectOfType<PathsHolder>();
        pathsHolder.CreatePath(wave);

        for (int i = 0; i < wave.NumberOfEnemies; i++)
        {
            EnemyMover enemy = Instantiate(wave.EnemyType).GetComponent<EnemyMover>();
            enemy.SetStartTile(wave.StartCoordinates);

            Vector3 spawnPoint = new Vector3(wave.SpawnCoordinates.x, 0, wave.SpawnCoordinates.y);
            enemy.transform.position = spawnPoint;

            enemy.gameObject.SetActive(true);

            yield return new WaitForSeconds(wave.SpawnRate);
        }
    }
}
