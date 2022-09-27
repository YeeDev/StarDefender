using UnityEngine;

namespace StarDef.Tutorials
{
    public class TutorialObject : MonoBehaviour
    {
        [SerializeField] ObjectTag tutorialTag = ObjectTag.NONE;

        bool pressed;
        [SerializeField]bool inTutorial;

        public bool InTutorial { get => inTutorial; }
        public ObjectTag GetTag { get => tutorialTag; }
        public bool Pressed { get => pressed; }

        public void InteractWithObject() { pressed = true; }
        public void TurnTurotialOn() { inTutorial = true; }
        public void ResetObject()
        {
            inTutorial = false;
            pressed = false;
        }
    }
}