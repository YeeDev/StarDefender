using System.Collections;
using StarDef.Info;

namespace StarDef.GameSequences
{
    public interface ISequence
    {
        public IEnumerator PlaySequence(SequenceVariableHolder infoHolder);
    }
}