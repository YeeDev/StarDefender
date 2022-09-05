using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Ship : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] float rotationSpeed = 3f;
    [Header("Debug, remove later")]
    [SerializeField] Transform[] pathArray = null;

    int objectToMoveTo = 0;
    bool rotating;
    Animator anm;

    private void Awake() { anm = GetComponent<Animator>(); }

    private void Start()
    {
        Vector3 positionWithHeight = pathArray[0].position;
        positionWithHeight.y = transform.position.y;

        transform.LookAt(positionWithHeight, Vector3.up);
    }

    private void Update() { MoveShip(); }

    private void MoveShip()
    {
        Vector3 positionWithHeight = pathArray[objectToMoveTo].position;
        positionWithHeight.y = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, positionWithHeight, speed * Time.deltaTime);

        RotateShip(positionWithHeight);
        SetNextTilePosition(positionWithHeight);
    }

    private void RotateShip(Vector3 positionWithHeight)
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
