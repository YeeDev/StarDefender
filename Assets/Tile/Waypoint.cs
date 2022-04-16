using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool canPlaceTower = false;
    [SerializeField] bool isCorner = false;

    Animator animator;

    public bool GetIsCorner { get => isCorner; }

    private void Awake()
    {
        if (canPlaceTower) { animator = GetComponent<Animator>(); }
    }

    private void OnMouseDown()
    {
        if (canPlaceTower)
        {
            animator.SetBool("IsOpen", true);
        }
    }
}