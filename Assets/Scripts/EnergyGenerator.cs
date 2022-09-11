using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyGenerator : MonoBehaviour
{
    [SerializeField] MeshRenderer indicator = null;
    [SerializeField] Color damagedColor = Color.red;

    public void DamageGenerator()
    {
       indicator.material.color = damagedColor;
    }
}