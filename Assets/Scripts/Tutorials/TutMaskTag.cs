using UnityEngine;

namespace StarDef.Tutorials
{
    public class TutMaskTag : MonoBehaviour
    {
        [SerializeField] MaskTag tutorialTag = MaskTag.Tutorial_0;
        Animator animator;

        public MaskTag GetTag { get => tutorialTag; }
        public Animator GetAnimator { get => animator; }

        private void Awake() { if (tutorialTag == MaskTag.Tutorial_Mask) { animator = GetComponent<Animator>(); } }
    }
}