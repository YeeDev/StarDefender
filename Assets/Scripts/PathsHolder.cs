using System.Collections.Generic;
using UnityEngine;

public class PathsHolder : MonoBehaviour
{
    [SerializeField] Path[] paths = null;

    PathFinder pathFinder;

    private void Awake()
    {
        pathFinder = GetComponent<PathFinder>();

        CreatePaths();
    }

    private void CreatePaths()
    {
        foreach (Path path in paths)
        {
            path.SetPath = pathFinder.CreatePath(path.StartTile, path.EndTile);
        }
    }

    public List<Tile> GetPath(Tile startingTile)
    {
        foreach (Path path in paths)
        {
            if (path.GetPath.Contains(startingTile))
            {
                return path.GetPath;
            } 
        }

        Debug.LogWarning("Error: There's no path.");
        return null;
    }
}
