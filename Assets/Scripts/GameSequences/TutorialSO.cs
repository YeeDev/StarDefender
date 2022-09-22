using System.Collections;
using StarDef.Info;
using UnityEngine;
using StarDef.Tutorials;

namespace StarDef.GameSequences
{
    [CreateAssetMenu(fileName = "New Tutorial", menuName = "Tutorial")]
    public class TutorialSO : ScriptableObject, ISequence
    {
        [SerializeField] Tag tutorialObject = Tag.Tutorial_0;

        public IEnumerator PlaySequence(SequenceVariableHolder infoHolder)
        {
            Animator tutorialFader = infoHolder.TutorialMask.GetAnimator;
            GameObject usedMask = infoHolder.GetTutorialObject(tutorialObject).gameObject;

            tutorialFader.SetBool("InTutorial", true);
            usedMask.SetActive(true);

            yield return new WaitForSeconds(1f);

            tutorialFader.SetBool("InTutorial", false);

            yield return new WaitForSeconds(0.5f);

            usedMask.SetActive(false);
        }
    }
}