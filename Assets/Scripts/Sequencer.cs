using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequencer : MonoBehaviour
{
    [SerializeField] WaveSO[] waves;

    private void Start()
    {
        StartCoroutine(RunWaves());
    }

    private IEnumerator RunWaves()
    {
        foreach (WaveSO wave in waves)
        {
            yield return wave.PlayWave();
        }
    }
}
