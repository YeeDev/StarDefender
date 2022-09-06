using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO Add to a namespace
//TODO Ships need to die
[RequireComponent(typeof(Animator))]
public class Ship : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] float rotationSpeed = 3f;
    [SerializeField] float missileSpeed = 2.5f;
    [SerializeField] GameObject missilePrefab = null;
    [SerializeField] Transform hardpoint = null;
    [Header("Debug, remove later")]
    [SerializeField] Transform[] pathArray = null;

    int objectToMoveTo = 0;
    bool rotating;
    bool missileFired;
    Animator anm;

    private void Awake() { anm = GetComponent<Animator>(); }

    private void Update() { MoveShip(); }

    private void MoveShip()
    {
        Vector3 positionWithHeight = pathArray[objectToMoveTo].position;
        positionWithHeight.y = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, positionWithHeight, speed * Time.deltaTime);

        CheckRotationType(positionWithHeight);
        SetNextTilePosition(positionWithHeight); //TODO this shouldn't be here

        if (objectToMoveTo + 1 == pathArray.Length && !missileFired) { Shoot(); }
    }

    private void CheckRotationType(Vector3 positionWithHeight)
    {
        if (pathArray[objectToMoveTo].CompareTag("Corner")) { RotateSmoothly(); }
        else { transform.LookAt(positionWithHeight, Vector3.up); }
    }

    private void RotateSmoothly()
    {
        Vector3 normalizedTilePosition = pathArray[objectToMoveTo + 1].position - transform.position;
        normalizedTilePosition.y = transform.position.y;

        Quaternion rot = Quaternion.LookRotation(normalizedTilePosition);

        if (!rotating) { StartCoroutine(AnimateTurn(normalizedTilePosition)); }

        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * rotationSpeed);
    }

    private IEnumerator AnimateTurn(Vector3 normalizedTilePosition)
    {
        rotating = true;

        if (Vector3.Dot(transform.right, normalizedTilePosition) < 0) { anm.SetTrigger("TurningLeft"); }
        else { anm.SetTrigger("TurningRight"); }

        yield return new WaitForSeconds(1.1f);

        rotating = false;
    }

    private void Shoot()
    {
        missileFired = true;
        Rigidbody missile = Instantiate(missilePrefab, hardpoint.position, hardpoint.rotation).GetComponent<Rigidbody>();
        missile.velocity = transform.forward * missileSpeed;
    }

    //TODO Grab it from another place, not here
    private void SetNextTilePosition(Vector3 positionWithHeight)
    {
        if ((transform.position - positionWithHeight).sqrMagnitude <= 0)
        {
            if (objectToMoveTo + 1 >= pathArray.Length) { return; }

            objectToMoveTo++;
        }
    }
}
