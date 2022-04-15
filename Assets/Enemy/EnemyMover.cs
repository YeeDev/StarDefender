using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0f, 10f)] float speed = 1f;
    [SerializeField] List<Waypoint> path = new List<Waypoint>();

    private void Start()
    {
        StartCoroutine(FollowPath());
    }

    private IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            endPosition.y = transform.position.y;
            float travelPercent = 0f;
            Quaternion quaternion = Quaternion.LookRotation(waypoint.transform.position - transform.position, Vector3.up);
            Vector3 euler = Vector3.zero;
            euler.y = quaternion.eulerAngles.y;
            transform.rotation = Quaternion.Euler(euler);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);

                yield return new WaitForEndOfFrame();
            }
        }
    }
}
