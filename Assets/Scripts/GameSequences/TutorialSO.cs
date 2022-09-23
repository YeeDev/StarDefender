using System.Collections;
using StarDef.Info;
using UnityEngine;
using StarDef.Tutorials;

namespace StarDef.GameSequences
{
    [CreateAssetMenu(fileName = "New Tutorial", menuName = "Tutorial")]
    public class TutorialSO : ScriptableObject, ISequence
    {
        [SerializeField] float waitTESTTime = 20f;
        [SerializeField] MaskTag tutorialMask = MaskTag.Tutorial_0;
        [SerializeField] bool stopsTime = false;

        public IEnumerator PlaySequence(SequenceVariableHolder infoHolder)
        {
            if (stopsTime) { Time.timeScale = 0; }

            Animator tutorialFader = infoHolder.GetAnimatedMask.GetAnimator;
            GameObject usedMask = infoHolder.GetTutorialMask(tutorialMask).gameObject;

            tutorialFader.SetBool("InTutorial", true);
            usedMask.SetActive(true);

            yield return new WaitForSecondsRealtime(waitTESTTime);

            tutorialFader.SetBool("InTutorial", false);

            yield return new WaitForSecondsRealtime(0.5f);

            usedMask.SetActive(false);

            if (stopsTime) { Time.timeScale = 1; }
        }
    }
}