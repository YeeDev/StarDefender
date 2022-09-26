using System.Collections;
using UnityEngine;
using StarDef.Info;

namespace StarDef.GameSequences
{
    [CreateAssetMenu(fileName = "New Window Action", menuName = "Sequences/Static/Window Action")]
    public class WindowControllerSO : ScriptableObject, ISequence
    {
        [SerializeField] bool opensWindow = true;
        [SerializeField] float timeItTakes = 1f;

        public IEnumerator PlaySequence(SequenceVariableHolder infoHolder)
        {
            if (opensWindow) { infoHolder.Printer.ClearDialogue(infoHolder.CommanderText); }

            infoHolder.CommanderAnimator.SetBool("Open", opensWindow);

            yield return new WaitForSeconds(timeItTakes);
        }
    }
}