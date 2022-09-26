using System.Collections;
using StarDef.Info;
using UnityEngine;

namespace StarDef.GameSequences
{
    [CreateAssetMenu(fileName = "New Wait Enemies", menuName = "Wait No Enemies")]
    public class WaitNoEnemiesSO : MonoBehaviour, ISequence
    {
        public IEnumerator PlaySequence(SequenceVariableHolder infoHolder)
        {
            yield return new WaitUntil(() => infoHolder.GetActiveEnemies <= 0);
            yield return new WaitForSeconds(1f);
        }
    }
}