using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveOrganizer : MonoBehaviour
{
    [SerializeField] Wave[] waves = null;

    ObjectPool pool;

    private void Awake() { pool = FindObjectOfType<ObjectPool>(); } 

    private void Start() { StartCoroutine(ExecuteWaves()); }

    private IEnumerator ExecuteWaves()
    {
        foreach (Wave wave in waves)
        {
            yield return StartCoroutine(RunWave(wave));
        }

        Debug.Log("Waves Ended");
    }

    private IEnumerator RunWave(Wave waveToRun)
    {
        for (int i = 0; i < waveToRun.NumberOfEnemies; i++)
        {
            EnemyMover enemy = pool.GetEnemy();
            enemy.SetStartTile(waveToRun.StartCoordinates);

            Vector3 spawnPoint = new Vector3(waveToRun.SpawnCoordinates.x, enemy.transform.position.y, waveToRun.SpawnCoordinates.y);
            enemy.transform.position = spawnPoint;

            enemy.gameObject.SetActive(true);

            yield return new WaitForSeconds(waveToRun.SpawnRate);
        }
    }
}
