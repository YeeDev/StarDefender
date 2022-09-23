using UnityEngine;

namespace StarDef.Tutorials
{
    public class TutObjectTag : MonoBehaviour
    {
        [SerializeField] ObjectTag tutorialTag = ObjectTag.NONE;

        bool pressed;
        bool inTutorial;

        public bool InTutorial { get => inTutorial; }
        public ObjectTag GetTag { get => tutorialTag; }
        public bool Pressed { get => pressed; }

        public void InteractWithObject() { pressed = true; }
        public void ChangeInTutorialStatus() { inTutorial = !inTutorial; }
    }
}