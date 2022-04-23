using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveOrganizer : MonoBehaviour
{
    [SerializeField] Wave wave = null;

    ObjectPool pool;

    private void Awake() { pool = FindObjectOfType<ObjectPool>(); } 

    private void Start() { StartCoroutine(RunWave()); }

    private IEnumerator RunWave()
    {
        for (int i = 0; i < wave.NumberOfEnemies; i++)
        {
            EnemyMover enemy = pool.GetEnemy();
            enemy.SetStartTile(wave.StartCoordinates);

            Vector3 spawnPoint = new Vector3(wave.SpawnCoordinates.x, enemy.transform.position.y, wave.SpawnCoordinates.y);
            enemy.transform.position = spawnPoint;

            enemy.gameObject.SetActive(true);

            yield return new WaitForSeconds(wave.SpawnRate);
        }
    }
}
