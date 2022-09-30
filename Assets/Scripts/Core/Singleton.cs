using UnityEngine;

namespace StarDef.Core
{
    public class Singleton : MonoBehaviour
    {
        bool isSingleton;

        public bool IsSingleton { get => isSingleton; }

        private void Awake()
        {
            foreach (Singleton singleton in FindObjectsOfType<Singleton>())
            {
                if (singleton.isSingleton && singleton != this)
                {
                    Destroy(gameObject);
                }
            }

            DontDestroyOnLoad(this);
            isSingleton = true;
        }
    }
}