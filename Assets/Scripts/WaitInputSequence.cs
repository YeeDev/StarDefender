using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Wait Input Sequnce", menuName ="Wait Input Sequence")]
public class WaitInputSequence : ScriptableObject, IGameSequence
{
    public IEnumerator PlaySequence(DialoguePrinter printer, Text text)
    {
        yield return new WaitUntil(() => Input.anyKeyDown);
    }
}
