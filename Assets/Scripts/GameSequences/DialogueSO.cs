using System.Collections;
using StarDef.Info;
using UnityEngine;

namespace StarDef.GameSequences
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Sequences/Dialogue")]
    public class DialogueSO : ScriptableObject, ISequence
    {
        [SerializeField] [TextArea(2, 5)] string dialogue = null;
        [SerializeField] ScriptableObject beforePrintDecorator = null;
        [SerializeField] ScriptableObject afterPrintDecorator = null;

        public IEnumerator PlaySequence(SequenceVariableHolder infoHolder)
        {
            ISequence decoratorSequence;

            if(beforePrintDecorator != null)
            {
                decoratorSequence = (ISequence)beforePrintDecorator;
                yield return decoratorSequence.PlaySequence(infoHolder);
            }

            yield return infoHolder.Printer.PrintDialogue(dialogue, infoHolder.CommanderText);

            if (afterPrintDecorator != null)
            {
                decoratorSequence = (ISequence)afterPrintDecorator;
                yield return decoratorSequence.PlaySequence(infoHolder);
            }
        }
    }
}