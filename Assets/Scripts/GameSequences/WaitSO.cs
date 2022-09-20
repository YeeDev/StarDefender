using System.Collections;
using UnityEngine;

namespace StarDef.GameSequences
{
    [CreateAssetMenu(fileName = "New Wait", menuName = "Wait")]
    public class WaitSO : ScriptableObject, ISequence
    {
        [SerializeField][Range(0, 10)] float timeToWait = 0f;

        public IEnumerator PlaySequence() { yield return new WaitForSeconds(timeToWait); }
    }
}