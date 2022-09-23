using UnityEngine;

namespace StarDef.Tutorials
{
    public class TutObjectTag : MonoBehaviour
    {
        [SerializeField] ObjectTag tutorialTag = ObjectTag.TutObject_0;

        bool pressed;

        public ObjectTag GetTag { get => tutorialTag; }
        public bool Pressed { get => pressed; }

        public void InteractWithObject() { pressed = true; }
    }
}