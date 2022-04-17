using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower tower = null;
    [SerializeField] bool isCorner = false;

    EnergyBank energyBank;

    public bool IsCorner { get => isCorner; }

    private void Awake() { energyBank = FindObjectOfType<EnergyBank>(); }

    private void OnMouseDown()
    {
        if (tower != null)
        {
            energyBank.AddActiveTower(tower.ActivateTower());
            energyBank.CheckIfDeactivateTower();
            energyBank.SetTowerIndicator();
        }
    }
}