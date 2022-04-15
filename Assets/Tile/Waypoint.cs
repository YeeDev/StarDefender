using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool canPlaceTower = false;

    private void OnMouseDown()
    {
        if (canPlaceTower)
        {
            Debug.Log(transform.name);
        }
    }
}
