using System.Collections;
using UnityEngine;
using StarDef.Info;

namespace StarDef.GameSequences
{
    [CreateAssetMenu(fileName = "New Wait Input", menuName = "Sequences/Static/Wait Input")]
    public class WaitInputSO : ScriptableObject, ISequence
    {
        public IEnumerator PlaySequence(SequenceVariableHolder infoHolder)
        {
            return new WaitUntil(() => Input.anyKeyDown );
        }
    }
}