using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Color availableColor = Color.blue;
    [SerializeField] Color activatedColor = Color.green;
    [SerializeField] Color toDeactivateColor = Color.red;
    [SerializeField] MeshRenderer indicator = null;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        SetAvailableIndicator();
    }

    public Tower ActivateTower()
    {
        animator.SetBool("IsOpen", true);
        return this;
    }

    public void DeactivateTower()
    {
        animator.SetBool("IsOpen", false);
        SetAvailableIndicator();
    }

    public void SetAvailableIndicator() { indicator.material.color = availableColor; }
    public void SetActiveIndicator() { indicator.material.color = activatedColor; }
    public void SetToDeactivateIndicator() { indicator.material.color = toDeactivateColor; }
}
