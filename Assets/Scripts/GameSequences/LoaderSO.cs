using System.Collections;
using StarDef.Info;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StarDef.GameSequences
{
    [CreateAssetMenu(fileName = "New Loader", menuName = "Sequences/Static/Loader")]
    public class LoaderSO : ScriptableObject, ISequence
    {
        [SerializeField] bool loadNextLevel = true;

        public IEnumerator PlaySequence(SequenceVariableHolder infoHolder)
        {
            infoHolder.FaderAnimator.SetTrigger("Fade");

            yield return new WaitForSeconds(1f);

            int currentScene = SceneManager.GetActiveScene().buildIndex;
            int sceneToLoad = loadNextLevel ? (currentScene + 1) % SceneManager.sceneCountInBuildSettings : currentScene;

            SceneManager.LoadScene(sceneToLoad);
        }

        public void LoadNextLevelUI() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }
    }
}