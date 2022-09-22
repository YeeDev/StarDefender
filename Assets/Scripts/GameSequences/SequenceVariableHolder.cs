using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yee.Dialogue;
using StarDef.Tutorials;

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
        public TutorialTag TutorialMask { get => tutorialObjects[Tag.Tutorial_Mask]; }

        Dictionary<Tag, TutorialTag> tutorialObjects = new Dictionary<Tag, TutorialTag>();

        public SequenceVariableHolder()
        {
            printer = GameObject.FindObjectOfType<DialoguePrinter>();

            GameObject commanderWindow = GameObject.FindGameObjectWithTag("CommanderWindow");
            commanderAnimator = commanderWindow.GetComponent<Animator>();
            commanderText = commanderWindow.GetComponentInChildren<Text>();

            CreateTutorialDictionary();
        }

        private void CreateTutorialDictionary()
        {
            foreach (TutorialTag tutorial in Resources.FindObjectsOfTypeAll<TutorialTag>())
            {
                if (tutorialObjects.ContainsKey(tutorial.GetTag))
                {
                    string dupObjectName = tutorial.transform.name;
                    string previousObjectName = tutorialObjects[tutorial.GetTag].transform.name;
                    Debug.LogWarning($"Duplicated tutorial tag: {dupObjectName} same as {previousObjectName}");
                    continue;
                }

                tutorialObjects.Add(tutorial.GetTag, tutorial);
            }

            if (!tutorialObjects.ContainsKey(Tag.Tutorial_Mask))
            {
                Debug.LogWarning("No Tutorial Mask was found, tutorials won't work;");
            }
        }

        public TutorialTag GetTutorialObject(Tag tutorialTag)
        {
            if (tutorialObjects.ContainsKey(tutorialTag)) { return tutorialObjects[tutorialTag]; }

            Debug.LogError($"There's no tutorial object with tag {tutorialTag}.");
            return null;
        }
    }
}