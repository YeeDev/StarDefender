using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(1f, 100f)] float speed = 1f;
    [SerializeField] [Range(1f, 100f)] float rotationSpeed = 1f;
    [SerializeField] Transform muzzle = null;
    [SerializeField] float missileSpeed = 1f;
    [SerializeField] GameObject missilePrefab = null;

    Tile startTile;
    List<Tile> path;
    Animator animator;
    PathsHolder pathsHolder;
    PathFinder pathFinder;

    public void SetStartTile(Vector2Int coordinates) { startTile = pathFinder.GetTileByCoordinates(coordinates); }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        pathsHolder = FindObjectOfType<PathsHolder>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    private void OnEnable() { StartCoroutine(FollowPath());}

    private IEnumerator FollowPath()
    {
        yield return new WaitUntil(() => pathsHolder.GetPath(startTile) != null); //Prevents OnEnable and Awake racing issue.

        path = pathsHolder.GetPath(startTile);
        LookAtStartingTile();

        for (int i = 0; i < path.Count; i++)
        {
            Tile tile = path[i];

            Vector3 startPosition = transform.position;
            Vector3 endPosition = tile.transform.position;
            endPosition.y = transform.position.y;

            RotateShip(tile);

            float travelPercent = 0f;

            if (i == (path.Count - 1)) { animator.SetTrigger("Attack"); }

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);

                yield return new WaitForEndOfFrame();
            }

            if (tile.IsCorner) { yield return RotateShip(path[i + 1]); }
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

        if (Vector3.Dot(transform.right, lookTarget) < 0) { animator.SetTrigger("TurningLeft"); }
        else { animator.SetTrigger("TurningRight"); }

        float rotationPercent = 0f;
        while (rotationPercent < 1f)
        {
            rotationPercent += Time.deltaTime * rotationSpeed;
            transform.rotation = Quaternion.Lerp(startRotation, lookDirection, rotationPercent);

            yield return new WaitForEndOfFrame();
        }
    }

    //Called in Animation "Ship_Attack"
    private void Attack()
    {
        Rigidbody missile = Instantiate(missilePrefab, muzzle.position, Quaternion.identity).GetComponent<Rigidbody>();
        missile.velocity = transform.forward * missileSpeed;
    }
}