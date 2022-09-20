using System.Collections;

namespace StarDef.GameSequences
{
    public interface ISequence
    {
        public IEnumerator PlaySequence();
    }
}