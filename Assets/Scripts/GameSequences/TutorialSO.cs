using System.Collections;
using StarDef.Info;
using UnityEngine;
using StarDef.Tutorials;

namespace StarDef.GameSequences
{
    [CreateAssetMenu(fileName = "New Tutorial", menuName = "Tutorial")]
    public class TutorialSO : ScriptableObject, ISequence
    {
        [SerializeField] MaskTag tutorialMask = MaskTag.NONE;
        [SerializeField] ObjectTag tutorialObject = ObjectTag.NONE;

        public IEnumerator PlaySequence(SequenceVariableHolder infoHolder)
        {
            Animator tutorialFader = infoHolder.GetAnimatedMask.GetAnimator;
            GameObject usedMask = infoHolder.GetTutorialMask(tutorialMask).gameObject;
            TutObjectTag tutObject = infoHolder.GetTutorialObject(tutorialObject);

            tutorialFader.SetBool("InTutorial", true);
            usedMask.SetActive(true);

            if(tutObject != null)
            {
                tutObject.ChangeInTutorialStatus();
                yield return new WaitUntil(() => tutObject.Pressed);
            }

            tutorialFader.SetBool("InTutorial", false);

            yield return new WaitForSecondsRealtime(0.5f);

            usedMask.SetActive(false);
        }
    }
}