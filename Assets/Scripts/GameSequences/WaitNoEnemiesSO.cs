using System.Collections;
using StarDef.Info;
using UnityEngine;

namespace StarDef.GameSequences
{
    [CreateAssetMenu(fileName = "New Wait Enemies", menuName = "Sequences/Static/Wait No Enemies")]
    public class WaitNoEnemiesSO : ScriptableObject, ISequence
    {
        public IEnumerator PlaySequence(SequenceVariableHolder infoHolder)
        {
            yield return new WaitUntil(() => infoHolder.GetActiveEnemies <= 0 || infoHolder.Health.NoHealth);
            yield return new WaitForSeconds(1f);
        }
    }
}