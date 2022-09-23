using System.Collections;
using UnityEngine;
using StarDef.Info;

namespace StarDef.GameSequences
{
    [CreateAssetMenu(fileName = "New Control Enabler", menuName = "Control Enabler")]
    public class ControlEnablerSO : ScriptableObject, ISequence
    {
        [SerializeField] bool enableControl = false;

        public IEnumerator PlaySequence(SequenceVariableHolder infoHolder)
        {
            infoHolder.GameStarter.OnControlChange(enableControl);
            yield return new WaitForEndOfFrame();
        }
    }
}
