using System.Collections;
using UnityEngine;
using StarDef.Info;
using StarDef.Core;

namespace StarDef.GameSequences
{
    public class Sequencer : MonoBehaviour
    {
        [SerializeField] ScriptableObject[] gameSequence = null;

        HealthStat health;
        SequenceVariableHolder infoHolder;

        private void Awake()
        {
            health = FindObjectOfType<HealthStat>();
            infoHolder = new SequenceVariableHolder();
        }

        private void Start() { StartCoroutine(RunWaves()); }

        private IEnumerator RunWaves()
        {
            foreach (ISequence sequence in gameSequence)
            {
                if(health.NoHealth) { yield break; }
                yield return sequence.PlaySequence(infoHolder);
            }
        }
    }
}