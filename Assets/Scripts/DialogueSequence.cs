using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Dialogue Sequence", menuName = "Dialogue Sequence")]
public class DialogueSequence : ScriptableObject, IGameSequence
{
    [SerializeField] Dialogue dialogue = null;

    public IEnumerator PlaySequence(DialoguePrinter printer, Text text)
    {
        yield return printer.PrintDialogue(dialogue.GetDialogue, text);
    }
}
