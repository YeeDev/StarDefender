using UnityEngine;

namespace StarDef.Tutorials
{
    public class TutorialMask : MonoBehaviour
    {
        [SerializeField] MaskTag tutorialTag = MaskTag.NONE;
        Animator animator;

        public MaskTag GetTag { get => tutorialTag; }
        public Animator GetAnimator { get => animator; }

        private void Awake() { if (tutorialTag == MaskTag.Tutorial_Mask) { animator = GetComponent<Animator>(); } }
    }
}