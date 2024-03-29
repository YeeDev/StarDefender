using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yee.Dialogue;
using StarDef.Tutorials;
using StarDef.Core;
using StarDef.Paths;

namespace StarDef.Info
{
    public class SequenceVariableHolder
    {
        int totalEnemies;
        Text commanderText;
        Animator commanderAnimator;
        Animator messagesAnimator;
        Animator faderAnimator;
        Animator commanderHead;
        PathFinder pathFinder;
        HealthStat health;
        ControlEnabler controlEnabler;
        
        DialoguePrinter printer;

        public int GetActiveEnemies { get => totalEnemies; }
        public Text CommanderText { get => commanderText; }
        public Animator CommanderAnimator { get => commanderAnimator; }
        public Animator MessagesAnimator { get => messagesAnimator; }
        public Animator FaderAnimator { get => faderAnimator; }
        public Animator CommanderHead { get => commanderHead; }
        public PathFinder GetPathFinder { get => pathFinder; }
        public HealthStat Health { get => health; }
        public ControlEnabler ControlEnabler { get => controlEnabler; }
        public DialoguePrinter Printer { get => printer; }
        public TutorialMask GetAnimatedMask { get => tutorialMasks[MaskTag.Tutorial_Mask]; }

        Dictionary<MaskTag, TutorialMask> tutorialMasks = new Dictionary<MaskTag, TutorialMask>();
        Dictionary<ObjectTag, TutorialObject> tutorialObjects = new Dictionary<ObjectTag, TutorialObject>();

        public SequenceVariableHolder()
        {
            printer = GameObject.FindObjectOfType<DialoguePrinter>();

            messagesAnimator = GameObject.FindGameObjectWithTag("Messages").GetComponent<Animator>();
            faderAnimator = GameObject.FindGameObjectWithTag("Finish").GetComponent<Animator>();
            commanderHead = GameObject.FindGameObjectWithTag("Commander").GetComponent<Animator>();

            GameObject commanderWindow = GameObject.FindGameObjectWithTag("CommanderWindow");
            commanderAnimator = commanderWindow.GetComponent<Animator>();
            commanderText = commanderWindow.GetComponentInChildren<Text>();

            controlEnabler = GameObject.FindObjectOfType<ControlEnabler>();
            pathFinder = GameObject.FindObjectOfType<PathFinder>();
            health = GameObject.FindObjectOfType<HealthStat>();

            PopulateDictionaries();
        }

        private void PopulateDictionaries()
        {
            List<TutorialMask> tutMasks = CreateList<TutorialMask>(new List<TutorialMask>());
            foreach (TutorialMask item in tutMasks)
            {
                tutorialMasks.Add(item.GetTag, item);
                if (item.GetTag != MaskTag.Tutorial_Mask) { item.gameObject.SetActive(false); }
            }

            List<TutorialObject> tutObjects = CreateList<TutorialObject>(new List<TutorialObject>());
            foreach (TutorialObject item in tutObjects) { tutorialObjects.Add(item.GetTag, item); }
        }

        private List<TTutorialType> CreateList<TTutorialType>(List<TTutorialType> listToPopulate)
        {
            foreach (var item in GameObject.FindObjectsOfType(typeof(TTutorialType)) as TTutorialType[])
            {
                listToPopulate.Add(item);
            }

            return listToPopulate;
        }

        public TutorialMask GetTutorialMask(MaskTag maskTag)
        {
            if (tutorialMasks.ContainsKey(maskTag)) { return tutorialMasks[maskTag]; }

            Debug.LogError($"There's no tutorial mask with tag {maskTag}.");
            return null;
        }

        public TutorialObject GetTutorialObject(ObjectTag objectTag)
        {
            if (tutorialObjects.ContainsKey(objectTag)) { return tutorialObjects[objectTag]; }

            Debug.LogError($"There's no tutorial object with tag {objectTag}.");
            return null;
        }

        public void AddActiveEnemy() { totalEnemies++; }
        public void RemoveActiveEnemy() { totalEnemies--; }
    }
}