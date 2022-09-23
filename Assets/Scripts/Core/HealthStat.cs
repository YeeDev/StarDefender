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

            if (totalGenerators <= 0) { Debug.Log("You lost!"); } //TODO debug purposes, delete later
        }
    }
}