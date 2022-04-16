using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] float attackRange = 5f;
    [SerializeField] Transform target = null;
    [SerializeField] Transform towerHead = null;
    [SerializeField] LayerMask enemyLayer = 0;

    Transform mainTarget = null;

    private void Update()
    {
        LocateClosestTarget();
        towerHead.LookAt(mainTarget);
    }

    private void LocateClosestTarget()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (Collider enemy in enemiesInRange)
        {
            float enemyDistance = (enemy.transform.position - transform.position).sqrMagnitude;

            if (enemyDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = enemyDistance;
            }
        }

        mainTarget = closestTarget;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
