using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] List<ScriptableObject> gameSequence = null;
    [SerializeField] Text text = null;
    [SerializeField] DialoguePrinter printer = null;
    [SerializeField] Animator commanderAnimator = null;

    private void Start()
    {
        StartCoroutine(PlayGameSequence());
    }

    IEnumerator PlayGameSequence()
    {
        commanderAnimator.SetBool("IsOpen", true);

        yield return new WaitForSeconds(0.75f);

        foreach (IGameSequence sequence in gameSequence)
        {
            yield return sequence.PlaySequence(printer, text);
        }

        commanderAnimator.SetBool("IsOpen", false);
    }
}
