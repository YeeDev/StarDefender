using UnityEngine;

namespace StarDef.Core
{
    public class HealthStat : MonoBehaviour
    {
        int totalHealth;

        public bool NoHealth { get => totalHealth <= 0; }

        public void AddHealth(int amountToAdd) { totalHealth += amountToAdd; }

        public void TakeDamage() { totalHealth--; }
    }
}