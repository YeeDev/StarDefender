using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarDef.Tiles
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] float attackRange = 1f;
        [SerializeField] LayerMask enemyLayer = 0;
        [SerializeField] Transform towerHead = null;

        Animator animator;

        Transform mainTarget;

        private void Awake() { animator = GetComponent<Animator>(); }

        private void OnMouseDown() { animator.SetBool("IsOpen", !animator.GetBool("IsOpen")); }

        private void Update()
        {
            if (!animator.GetBool("IsOpen"))
            {
                towerHead.rotation = Quaternion.identity;
                return;
            }

            LocateClosestTarget();
            ShootTarget();
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
            towerHead.LookAt(mainTarget);
            animator.SetBool("IsShooting", mainTarget != null);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}