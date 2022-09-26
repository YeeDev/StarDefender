using UnityEngine;
using StarDef.Tiles;

namespace StarDef.Core
{
    public class HealthStat : MonoBehaviour
    {
        int totalGenerators;

        public bool NoHealth { get => totalGenerators <= 0; }

        private void Awake() { totalGenerators = FindObjectsOfType<EnergyGenerator>().Length; }

        public void TakeDamage(EnergyGenerator energyGenerator)
        {
            if (energyGenerator.IsOn)
            {
                energyGenerator.DamageGenerator();
                totalGenerators--;
            }
        }
    }
}