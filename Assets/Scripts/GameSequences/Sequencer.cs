using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarDef.GameSequences
{
    public class Sequencer : MonoBehaviour
    {
        [SerializeField] ScriptableObject[] gameSequence = null;

        HealthStat health;

        private void Awake()
        {
            health = FindObjectOfType<HealthStat>();
        }

        private void Start()
        {
            StartCoroutine(RunWaves());
        }

        private IEnumerator RunWaves()
        {
            foreach (ISequence sequence in gameSequence)
            {
                if(health.NoHealth) { yield break; }
                yield return sequence.PlaySequence();
            }
        }
    }
}