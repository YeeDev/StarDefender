using System.Collections;
using StarDef.Info;
using UnityEngine;

namespace StarDef.GameSequences
{
    [CreateAssetMenu(fileName = "New End Message", menuName = "Sequences/Static/End Message")]
    public class EndMessageSO : ScriptableObject, ISequence
    {
        [SerializeField] bool showWinMessage = true;

        public IEnumerator PlaySequence(SequenceVariableHolder infoHolder)
        {
            string messageToShow = showWinMessage ? "Win" : "Lose";
            infoHolder.MessagesAnimator.SetTrigger(messageToShow);

            yield return null;
        }
    }
}