using System.Collections;
using System.Collections.Generic;
using StarDef.Info;
using UnityEngine;
using Yee.Dialogue;

namespace StarDef.GameSequences
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
    public class DialogueSO : ScriptableObject, ISequence
    {
        [SerializeField] [TextArea(2, 5)] string dialogue = null;
        [SerializeField] float waitBeforePrint = 1f;
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