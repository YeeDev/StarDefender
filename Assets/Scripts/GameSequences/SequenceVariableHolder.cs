using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yee.Dialogue;

namespace StarDef.Info
{
    public class SequenceVariableHolder
    {
        Text commanderText;
        Animator commanderAnimator;
        DialoguePrinter printer;

        public Text CommanderText { get => commanderText; }
        public Animator CommanderAnimator { get => commanderAnimator; }
        public DialoguePrinter Printer { get => printer; }

        List<object> debugList = new List<object>();

        public SequenceVariableHolder()
        {
            printer = GameObject.FindObjectOfType<DialoguePrinter>();

            GameObject commanderWindow = GameObject.FindGameObjectWithTag("CommanderWindow");
            commanderAnimator = commanderWindow.GetComponent<Animator>();
            commanderText = commanderWindow.GetComponentInChildren<Text>();

            foreach (object item in debugList)
            {
                if(item == null) { Debug.Log(item + "equals null"); }
            }
        }
    }
}