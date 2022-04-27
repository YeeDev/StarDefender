using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    int currentLevel;
    StationHealth stationHealth;

    private void Awake()
    {
        stationHealth = FindObjectOfType<StationHealth>();
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnEnable() { stationHealth.OnTakeDamage += ReloadLevel; }
    private void OnDisable() { stationHealth.OnTakeDamage -= ReloadLevel; }

    private void ReloadLevel() { if (stationHealth.GetHealthPoints <= 0) { LoadLevel(currentLevel); } }

    private void LoadLevel(int levelToLoad) { SceneManager.LoadScene(levelToLoad); }
}
