using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarDef.Info;

namespace StarDef.Enemies
{
    [RequireComponent(typeof(Animator))]
    public class Ship : MonoBehaviour
    {
        [SerializeField] float speed = 2f;
        [SerializeField] int health = 5;
        [SerializeField] float rotationSpeed = 3f;
        [SerializeField] float missileSpeed = 2.5f;
        [SerializeField] GameObject missilePrefab = null;
        [SerializeField] Transform hardpoint = null;
        [SerializeField] GameObject explosionParticles = null;

        int objectToMoveTo = 0;
        bool rotating;
        bool missileFired;
        Animator anm;
        List<Transform> path = null;
        SequenceVariableHolder infoHolder;

        public List<Transform> SetPath { set => path = value; }
        public SequenceVariableHolder SetInfoHolder { set => infoHolder = value; }

        private void Awake()
        {
            anm = GetComponent<Animator>();
            health *= 2; //Prevents prewarm bug.
        }

        private void Update() { if (path != null) { MoveShip(); } }

        private void OnParticleCollision(GameObject other) { TakeDamage(); }

        private void MoveShip()
        {
            Vector3 positionWithHeight = path[objectToMoveTo].position;
            positionWithHeight.y = transform.position.y;
            transform.position = Vector3.MoveTowards(transform.position, positionWithHeight, speed * Time.deltaTime);

            CheckRotationType(positionWithHeight);

            if (objectToMoveTo + 1 < path.Count) { SetNextTile((transform.position - positionWithHeight).sqrMagnitude); }
            else if (!missileFired) { Shoot(); }
        }

        private void CheckRotationType(Vector3 positionWithHeight)
        {
            if (path[objectToMoveTo].CompareTag("Corner")) { RotateSmoothly(); }
            else { transform.LookAt(positionWithHeight, Vector3.up); }
        }

        private void RotateSmoothly()
        {
            Vector3 normalizedTilePosition = path[objectToMoveTo + 1].position - transform.position;
            normalizedTilePosition.y = transform.position.y;

            Quaternion rot = Quaternion.LookRotation(normalizedTilePosition);

            if (!rotating) { StartCoroutine(AnimateTurn(normalizedTilePosition)); }

            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * rotationSpeed);
        }

        private IEnumerator AnimateTurn(Vector3 normalizedTilePosition)
        {
            rotating = true;

            string animationToPlay = Vector3.Dot(transform.right, normalizedTilePosition) < 0 ? "TurningLeft" : "TurningRight";
            anm.SetTrigger(animationToPlay);

            yield return new WaitForSeconds(1.1f);

            rotating = false;
        }

        private void SetNextTile(float sqrDistance) { if (sqrDistance <= 0) { objectToMoveTo++; } }

        private void Shoot()
        {
            missileFired = true;

            Rigidbody missile = Instantiate(missilePrefab, hardpoint.position, hardpoint.rotation).GetComponent<Rigidbody>();
            missile.velocity = transform.forward * missileSpeed;

            anm.SetTrigger("Fire");
        }

        private void TakeDamage()
        {
            health--;
            if (health <= 0)
            {
                Instantiate(explosionParticles, transform.position, Quaternion.identity);
                Remove();
            }
        }

        //Also Called in Animation
        private void Remove()
        {
            infoHolder.RemoveActiveEnemy();
            Destroy(gameObject);
        }
    }
}