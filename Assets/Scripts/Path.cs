using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Path
{
    Vector2Int startCoordinates;
    Vector2Int endCoordinates;

    List<Tile> path;

    public Path() { path = new List<Tile>(); }
    public Path(Vector2Int startCoordinates, Vector2Int endCoordinates)
    {
        this.startCoordinates = startCoordinates;
        this.endCoordinates = endCoordinates;
    }

    public Vector2Int GetStartCoordinates { get => startCoordinates; }
    public Vector2Int GetEndCoordinates { get => endCoordinates; }
    public List<Tile> GetPath { get => path; }
    public List<Tile> SetPath { set => path = value; }
}
