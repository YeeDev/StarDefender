using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] float waitTime = 1f;
    [SerializeField] List<Waypoint> path = new List<Waypoint>();

    private void Start()
    {
        StartCoroutine(FollowPath());
    }

    private IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in path)
        {
            Vector3 point = waypoint.transform.position;
            point.y = transform.position.y;
            transform.position = point;
            yield return new WaitForSeconds(waitTime);
        }
    }
}
