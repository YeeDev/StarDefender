using System.Collections;
using UnityEngine;
using StarDef.Info;
using StarDef.Core;

namespace StarDef.GameSequences
{
    public class Sequencer : MonoBehaviour
    {
        [SerializeField] ScriptableObject[] gameSequence = null;
        [SerializeField] ScriptableObject[] loseSequence = null;

        [Header("Testing Options")]
        [SerializeField] ScriptableObject[] testSequence = null;

        SequenceVariableHolder infoHolder;

        private void Awake() { infoHolder = new SequenceVariableHolder(); }

        private void Start() { StartCoroutine(RunSequences()); }

        private IEnumerator RunSequences()
        {
            StartCoroutine(RunLoseSequence());

            ScriptableObject[] sequenceToUse = testSequence.Length > 0 ? testSequence : gameSequence;

            foreach (ISequence sequence in sequenceToUse)
            {
                if (infoHolder.Health.NoHealth) { yield break; }

                yield return sequence.PlaySequence(infoHolder);
            }
        }

        private IEnumerator RunLoseSequence()
        {
            yield return new WaitUntil(() => infoHolder.Health.NoHealth);

            foreach (ISequence sequence in loseSequence)
            {
                yield return sequence.PlaySequence(infoHolder);
            }
        }
    }
}