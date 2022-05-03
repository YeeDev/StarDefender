using System.Collections;
using UnityEngine.UI;

public interface IGameSequence
{
    IEnumerator PlaySequence(DialoguePrinter printer, Text text);
}
