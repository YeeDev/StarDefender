using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] float attackRange = 5f;
    [SerializeField] Transform target = null;
    [SerializeField] Transform towerHead = null;
    [SerializeField] LayerMask enemyLayer = 0;

    Animator animator;
    AudioSource audioSource;
    Transform mainTarget = null;

    //Called in animation
    private void PlayLaserShoot() { audioSource.Play(); }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!animator.GetBool("IsOpen")) { return; }

        LocateClosestTarget();
        ShootTarget();
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

    private void ShootTarget()
    {
        animator.SetBool("IsShooting", mainTarget != null);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
