using System.Collections.Generic;
using UnityEngine;

public class PathsHolder : MonoBehaviour
{
    List<Path> paths = new List<Path>();
    WaveOrganizer waveOrganizer;
    PathFinder pathFinder;

    private void Awake()
    {
        pathFinder = GetComponent<PathFinder>();
        waveOrganizer = FindObjectOfType<WaveOrganizer>();

        CreatePaths();
    }

    private void CreatePaths()
    {
        foreach (Wave wave in waveOrganizer.GetWaves)
        {
            Path path = new Path(wave.StartCoordinates, wave.GoalCoordinates);
            path.SetPath = pathFinder.CreatePath(path);
            paths.Add(path);
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

        Debug.LogWarning("Still Calculating Path");
        return null;
    }
}
