using System.Collections;
using StarDef.Info;
using UnityEngine;
using StarDef.Tutorials;

namespace StarDef.GameSequences
{
    [CreateAssetMenu(fileName = "New Tutorial", menuName = "Sequences/Tutorial")]
    public class TutorialSO : ScriptableObject, ISequence
    {
        [SerializeField] [Range(0, 10)] int timeToWait = 0;
        [SerializeField] MaskTag tutorialIndicator = MaskTag.NONE;
        [SerializeField] ObjectTag tutorialObject = ObjectTag.NONE;

        public IEnumerator PlaySequence(SequenceVariableHolder infoHolder)
        {
            Animator tutorialFader = infoHolder.GetAnimatedMask.GetAnimator;
            GameObject usedMask = infoHolder.GetTutorialMask(tutorialIndicator).gameObject;
            TutorialObject tutObject = infoHolder.GetTutorialObject(tutorialObject);

            tutorialFader.SetBool("InTutorial", true);
            usedMask.SetActive(true);

            if(tutorialObject != ObjectTag.NONE && tutObject != null)
            {
                tutObject.ChangeInTutorialStatus();
                yield return new WaitUntil(() => tutObject.Pressed);
                tutObject.ChangeInTutorialStatus();
            }

            if (timeToWait > 0) { yield return new WaitForSeconds(timeToWait); }

            tutorialFader.SetBool("InTutorial", false);

            yield return new WaitForSecondsRealtime(0.5f);

            usedMask.SetActive(false);
        }
    }
}