using System.Collections;
using UnityEngine;
using StarDef.Info;

namespace StarDef.GameSequences
{
    [CreateAssetMenu(fileName = "New Wait", menuName = "Sequences/Wait")]
    public class WaitSO : ScriptableObject, ISequence
    {
        [SerializeField][Range(0, 10)] float timeToWait = 0f;

        public IEnumerator PlaySequence(SequenceVariableHolder infoHolder) { yield return new WaitForSeconds(timeToWait); }
    }
}