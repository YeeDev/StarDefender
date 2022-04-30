using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBank : MonoBehaviour
{
    [SerializeField] int maxActiveTowers = 1;

    List<Tower> activeTowers = new List<Tower>();

    public int GetMaxActiveTowers { get => maxActiveTowers; }

    //Called in Waypoint
    public bool AddActiveTower(Tower towerToAdd)
    {
        if (activeTowers.Contains(towerToAdd))
        {
            towerToAdd.DeactivateTower();
            activeTowers.Remove(towerToAdd);

            if (activeTowers.Count > 0) { activeTowers.First().SetActiveIndicator(); }

            return false;
        }

        activeTowers.Add(towerToAdd);
        towerToAdd.SetActiveIndicator();

        return true;
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