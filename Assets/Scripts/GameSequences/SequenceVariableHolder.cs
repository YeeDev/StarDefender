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
        public TutMaskTag GetAnimatedMask { get => tutorialMasks[MaskTag.Tutorial_Mask]; }

        Dictionary<MaskTag, TutMaskTag> tutorialMasks = new Dictionary<MaskTag, TutMaskTag>();
        Dictionary<ObjectTag, TutObjectTag> tutorialObjects = new Dictionary<ObjectTag, TutObjectTag>();

        public SequenceVariableHolder()
        {
            printer = GameObject.FindObjectOfType<DialoguePrinter>();

            GameObject commanderWindow = GameObject.FindGameObjectWithTag("CommanderWindow");
            commanderAnimator = commanderWindow.GetComponent<Animator>();
            commanderText = commanderWindow.GetComponentInChildren<Text>();

            PopulateDictionaries();
        }

        private void PopulateDictionaries()
        {
            List<TutMaskTag> tutMasks = CreateList<TutMaskTag>(new List<TutMaskTag>());
            foreach (TutMaskTag item in tutMasks) { tutorialMasks.Add(item.GetTag, item); }

            List<TutObjectTag> tutObjects = CreateList<TutObjectTag>(new List<TutObjectTag>());
            foreach (TutObjectTag item in tutObjects) { tutorialObjects.Add(item.GetTag, item); }
        }

        private List<TTutorialType> CreateList<TTutorialType>(List<TTutorialType> listToPopulate)
        {
            foreach (var item in Resources.FindObjectsOfTypeAll(typeof(TTutorialType)) as TTutorialType[])
            {
                listToPopulate.Add(item);
            }

            return listToPopulate;
        }

        public TutMaskTag GetTutorialMask(MaskTag maskTag)
        {
            if (tutorialMasks.ContainsKey(maskTag)) { return tutorialMasks[maskTag]; }

            Debug.LogError($"There's no tutorial mask with tag {maskTag}.");
            return null;
        }

        public TutObjectTag GetTutorialObject(ObjectTag objectTag)
        {
            if (tutorialObjects.ContainsKey(objectTag)) { return tutorialObjects[objectTag]; }

            Debug.LogError($"There's no tutorial object with tag {objectTag}.");
            return null;
        }
    }
}