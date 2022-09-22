using UnityEngine;

namespace StarDef.Tutorials
{
    public class TutorialTag : MonoBehaviour
    {
        [SerializeField] Tag tutorialTag = Tag.Tutorial_0;

        Animator animator;

        public Tag GetTag { get => tutorialTag; }
        public Animator GetAnimator { get => animator; }

        private void Awake() { if (tutorialTag == Tag.Tutorial_Mask) { animator = GetComponent<Animator>(); } }
    }
}