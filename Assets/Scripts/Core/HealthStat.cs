using System.Collections.Generic;
using UnityEngine;
using StarDef.Tiles;

namespace StarDef.Core
{
    public class HealthStat : MonoBehaviour
    {
        Queue<EnergyGenerator> generators = new Queue<EnergyGenerator>();

        public bool NoHealth { get => generators.Count <= 0; }

        private void Awake()
        {
            EnergyGenerator[] generatorsFound = FindObjectsOfType<EnergyGenerator>();

            if (generatorsFound.Length <= 0)
            {
                Debug.LogError("Generators not found!");
                return;
            }

            foreach (var generator in FindObjectsOfType<EnergyGenerator>())
            {
                generators.Enqueue(generator);
            }
        }

        public void TakeDamage()
        {
            if (generators.Count <= 0) { return; }

            EnergyGenerator generator = generators.Dequeue();
            generator.DamageGenerator();

            if (generators.Count <= 0) { Debug.Log("You lost!"); }
        }
    }
}