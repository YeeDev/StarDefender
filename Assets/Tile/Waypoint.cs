using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool canPlaceTower = false;
    [SerializeField] bool isCorner = false;

    public bool GetIsCorner { get => isCorner; }

    private void OnMouseDown()
    {
        if (canPlaceTower)
        {
            Debug.Log(transform.name);
        }
    }
}