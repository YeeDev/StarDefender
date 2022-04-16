using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField] Transform towerHead = null;

    private void Update()
    {
        towerHead.LookAt(target);
    }
}
