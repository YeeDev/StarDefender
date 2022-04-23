using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0f, 10f)] float speed = 1f;
    [SerializeField] Animator shipAnimator = null;

    Tile startTile;
    List<Tile> path;
    PathsHolder pathsHolder;

    public Tile SetStartTile { set => startTile = value; }

    private void Awake() { pathsHolder = FindObjectOfType<PathsHolder>();}

    private void OnEnable() { StartCoroutine(FollowPath());}

    private IEnumerator FollowPath()
    {
        yield return new WaitUntil(() => pathsHolder.GetPath(startTile) != null); //Prevents OnEnable and Awake racing issue.

        path = pathsHolder.GetPath(startTile);
        LookAtStartingTile();

        int i = 0;
        foreach (Tile tile in path)
        {
            i++;
            Vector3 startPosition = transform.position;
            Vector3 endPosition = tile.transform.position;
            endPosition.y = transform.position.y;

            RotateShip(tile);

            float travelPercent = 0f;

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);

                yield return new WaitForEndOfFrame();
            }

            if (tile.IsCorner) { yield return RotateShip(path[i]); }
        }
    }

    private void LookAtStartingTile()
    {
        Vector3 lookTarget = path[0].transform.position - transform.position;
        lookTarget.y = 0;
        transform.rotation = Quaternion.LookRotation(lookTarget, Vector3.up);
    }

    private IEnumerator RotateShip(Tile tile)
    {
        Quaternion startRotation = transform.rotation;
        Vector3 lookTarget = tile.transform.position - transform.position;
        lookTarget.y = 0;
        Quaternion lookDirection = Quaternion.LookRotation(lookTarget, Vector3.up);

        if (Vector3.Dot(transform.right, lookTarget) < 0) { shipAnimator.SetTrigger("TurningLeft"); }
        else { shipAnimator.SetTrigger("TurningRight"); }

        float rotationPercent = 0f;
        while (rotationPercent < 1f)
        {
            rotationPercent += Time.deltaTime * speed;
            transform.rotation = Quaternion.Lerp(startRotation, lookDirection, rotationPercent);

            yield return new WaitForEndOfFrame();
        }
    }
}