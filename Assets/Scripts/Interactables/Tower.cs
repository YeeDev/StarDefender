using System.Collections.Generic;
using UnityEngine;
using StarDef.Paths;
using StarDef.Tiles;
using StarDef.Tutorials;
using StarDef.Core;

namespace StarDef.Interactables
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] float attackRange = 1f;
        [SerializeField] LayerMask enemyLayer = 0;
        [SerializeField] Transform towerHead = null;
        [SerializeField] Color enabledColor, disabledColor;

        bool gameHasStarted;
        Animator animator;
        EnergyFinder energyFinder;
        EnergyGenerator generator;
        AudioSource audioSource;
        TutorialObject tutorialTag;
        GameStarter gameStarter;
        List<MeshRenderer> indicators;

        Transform mainTarget;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
            tutorialTag = GetComponent<TutorialObject>();

            gameStarter = FindObjectOfType<GameStarter>();
            energyFinder = FindObjectOfType<EnergyFinder>();
        }

        private void Start()
        {
            indicators = energyFinder.FindEnergy(GetComponent<Tile>().GridCoordinates);
            generator = indicators[0].GetComponentInParent<EnergyGenerator>();
            generator.AddPath(indicators);
        }

        private void OnEnable() { gameStarter.OnGameStart += EnableControl; }
        private void OnDisable() { gameStarter.OnGameStart -= EnableControl; }

        private void EnableControl() { gameHasStarted = true; }

        private void OnMouseDown()
        {
            if (!gameHasStarted) { return; }

            if (!generator.IsOn || !generator.CanBeUsed(transform)) { return; }

            animator.SetBool("IsOpen", !animator.GetBool("IsOpen"));
            generator.UsedBy = animator.GetBool("IsOpen") ? transform : null;

            if (tutorialTag != null && tutorialTag.InTutorial) { tutorialTag.InteractWithObject(); }

            ChangeIndicatorsColor();
        }

        public void PlayLaserSound() { audioSource.Play(); }

        private void ChangeIndicatorsColor()
        {
            Color colorToUse = animator.GetBool("IsOpen") ? enabledColor : disabledColor;

            foreach (MeshRenderer indicator in indicators)
            {
                indicator.material.color = colorToUse;
            }
        }

        private void Update()
        {
            LookAtClosestTarget();
            ShootTarget();
        }

        private void LookAtClosestTarget()
        {
            if (!generator.IsOn) { return; }

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
            towerHead.LookAt(mainTarget);
        }

        private void ShootTarget()
        {
            animator.SetBool("IsShooting", mainTarget != null && animator.GetBool("IsOpen") && generator.IsOn);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}