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
        [SerializeField] Tag tutorialObject = Tag.Tutorial_0;
        [SerializeField] bool stopsTime = false;

        public IEnumerator PlaySequence(SequenceVariableHolder infoHolder)
        {
            if (stopsTime) { Time.timeScale = 0; }

            Animator tutorialFader = infoHolder.TutorialMask.GetAnimator;
            GameObject usedMask = infoHolder.GetTutorialObject(tutorialObject).gameObject;

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