using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commander : MonoBehaviour
{
    [SerializeField] [TextArea(2, 3)] string[] dialogues = null;

    DialoguePrinter printer;

    private void Awake() { printer = GetComponent<DialoguePrinter>(); }


}
