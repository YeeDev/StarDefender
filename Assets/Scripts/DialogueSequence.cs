using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Dialogue Sequence", menuName = "Dialogue Sequence")]
public class DialogueSequence : ScriptableObject, IGameSequence
{
    [SerializeField] Dialogue dialogue = null;
    [SerializeField] float timeToOpenWindow = 0.75f;
    [SerializeField] bool closesWindow = false;

    public IEnumerator PlaySequence(DialoguePrinter printer, Text text)
    {
        Animator animator = GameObject.FindGameObjectWithTag("Commander").GetComponent<Animator>();
        Debug.Log(animator.gameObject);

        if(!animator.GetBool("IsOpen"))
        {
            animator.SetBool("IsOpen", true);
            yield return new WaitForSeconds(timeToOpenWindow);
        }

        yield return printer.PrintDialogue(dialogue.GetDialogue, text);


        if (closesWindow)
        {
            yield return new WaitUntil(() => Input.anyKeyDown);

            animator.SetBool("IsOpen", false);
            yield return new WaitForSeconds(timeToOpenWindow);
        }
    }
}
