using System.Collections.Generic;
using UnityEngine;

public class PathsHolder : MonoBehaviour
{
    List<Path> paths = new List<Path>();
    PathFinder pathFinder;

    private void Awake() { pathFinder = GetComponent<PathFinder>(); }

    //Called in WaveSequence
    public void CreatePath(Wave wave)
    {
        Path path = new Path(wave.StartCoordinates, wave.GoalCoordinates);
        path.SetPath = pathFinder.CreatePath(path);
        paths.Add(path);
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
