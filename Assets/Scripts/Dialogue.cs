using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [SerializeField] [TextArea(2, 10)] string dialogue = null;

    public string GetDialogue { get => dialogue; }
}
