using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0f, 10f)] float speed = 1f;
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] Animator shipAnimator = null;

    private void Start() { StartCoroutine(FollowPath()); }

    private IEnumerator FollowPath()
    {
        int i = 0;
        foreach (Waypoint waypoint in path)
        {
            i++;
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            endPosition.y = transform.position.y;

            RotateShip(waypoint);

            float travelPercent = 0f;

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);

                yield return new WaitForEndOfFrame();
            }

            if (waypoint.GetIsCorner) { yield return RotateShip(path[i]); }
        }
    }

    private IEnumerator RotateShip(Waypoint waypoint)
    {
        Quaternion startRotation = transform.rotation;
        Vector3 lookTarget = waypoint.transform.position - transform.position;
        lookTarget.y = 0;
        Quaternion lookDirection = Quaternion.LookRotation(lookTarget, Vector3.up);

        if (Vector3.Dot(transform.right, lookTarget) < 0) { shipAnimator.SetTrigger("TurningLeft"); }
        else { shipAnimator.SetTrigger("TurningRight"); }

        float travelPercent = 0f;
        while (travelPercent < 1f)
        {
            travelPercent += Time.deltaTime * speed;
            transform.rotation = Quaternion.Lerp(startRotation, lookDirection, travelPercent);

            yield return new WaitForEndOfFrame();
        }
    }
}
