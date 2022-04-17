using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBank : MonoBehaviour
{
    [SerializeField] int maxActiveTowers = 1;

    List<Tower> activeTowers = new List<Tower>();

    //Called in Waypoint
    public void AddActiveTower(Tower towerToAdd)
    {
        activeTowers.Add(towerToAdd);
        towerToAdd.SetActiveIndicator();
    }

    //Called in Waypoint
    public void CheckIfDeactivateTower()
    {
        if (activeTowers.Count > maxActiveTowers)
        {
            activeTowers.First().DeactivateTower();
            activeTowers.Remove(activeTowers.First());
        }
    }

    //Called in Waypoint
    public void SetTowerIndicator()
    {
        if (activeTowers.Count == maxActiveTowers)
        {
            activeTowers.First().SetToDeactivateIndicator();
        }
    }
}