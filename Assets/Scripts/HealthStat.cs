using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthStat : MonoBehaviour
{
    Queue<EnergyGenerator> generators = new Queue<EnergyGenerator>();

    public bool NoHealth { get => generators.Count <= 0; }

    private void Awake()
    {
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
