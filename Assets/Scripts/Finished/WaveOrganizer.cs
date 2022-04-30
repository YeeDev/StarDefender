using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WaveOrganizer : MonoBehaviour
{
    [SerializeField] Wave[] waves = null;

    ObjectPool pool;

    public Wave[] GetWaves { get => waves; }

    private void Awake() { pool = FindObjectOfType<ObjectPool>(); } 

    private void Start() { StartCoroutine(ExecuteWaves()); }

    private IEnumerator ExecuteWaves()
    {
        foreach (Wave wave in waves)
        {
            yield return StartCoroutine(RunWave(wave));
        }

        yield return new WaitUntil(() => AreAllEnemiesInactive());

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

            if (i != waveToRun.NumberOfEnemies - 1) { yield return new WaitForSeconds(waveToRun.SpawnRate); }
        }
    }

    private bool AreAllEnemiesInactive()
    {
        List<Transform> enemies = pool.transform.Cast<Transform>().ToList();

        return enemies.FirstOrDefault(x => x.gameObject.activeInHierarchy) == null;
    }
}
