using System.Collections.Generic;
using UnityEngine;

public class PathsHolder : MonoBehaviour
{
    List<Path> paths = new List<Path>();
    PathFinder pathFinder;

    private void Awake()
    {
        pathFinder = GetComponent<PathFinder>();

        CreatePaths();
    }

    private void CreatePaths()
    {
        Debug.Log("This is heavily modified");
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

        Debug.LogWarning("Still Calculating Path");
        return null;
    }
}
