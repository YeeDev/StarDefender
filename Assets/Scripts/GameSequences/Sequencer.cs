using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarDef.GameSequences
{
    public class Sequencer : MonoBehaviour
    {
        [SerializeField] WaveSO[] waves;

        HealthStat health;

        private void Awake() { health = FindObjectOfType<HealthStat>(); }

        private void Start()
        {
            StartCoroutine(RunWaves());
        }

        private IEnumerator RunWaves()
        {
            foreach (WaveSO wave in waves)
            {
                if(health.NoHealth) { yield break; }
                yield return wave.PlayWave();
            }
        }
    }
}