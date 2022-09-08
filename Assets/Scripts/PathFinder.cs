using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO Namespace and create and actual path in awake, then let the ship grab it
public class PathFinder : MonoBehaviour
{
    [Tooltip("Unity's Grid Snap Settings")]
    [SerializeField] int unityGridSize = 1;
    [SerializeField] Vector2Int startCoordinate, endCoordinate;

    Vector2Int[] directions = { Vector2Int.right, Vector2Int.up, Vector2Int.down, Vector2Int.left };

    Queue<Tile> tilesToExplore = new Queue<Tile>();
    List<Transform> pathTiles = new List<Transform>();
    Dictionary<Vector2Int, Tile> grid = new Dictionary<Vector2Int, Tile>();

    public List<Transform> GetPath { get => pathTiles; } 

    private void Awake()
    {
        LoadTiles();
        FindPath();
    }

    private void LoadTiles()
    {
        var waypoints = FindObjectsOfType<Tile>();
        foreach (Tile tile in waypoints)
        {
            var gridPos = tile.GridCoordinates;
            if (grid.ContainsKey(gridPos)) { Debug.LogWarning("Skipping overlapping block " + tile); }
            else { grid.Add(gridPos, tile); }
        }
    }

    private void FindPath()
    {
        tilesToExplore.Enqueue(grid[startCoordinate]);

        while (tilesToExplore.Count > 0)
        {
            Tile exploringTile = tilesToExplore.Dequeue();

            ExploreNeighbours(exploringTile);

            if (exploringTile.GridCoordinates == endCoordinate)
            {
                tilesToExplore.Clear();
            }
        }

        CreatePath();
    }

    private void ExploreNeighbours(Tile exploredTile)
    {
        exploredTile.AlreadyExplored = true;

        foreach (Vector2Int direction in directions)
        {
            Vector2Int positionToSearch = exploredTile.GridCoordinates + direction;
            if (grid.ContainsKey(positionToSearch))
            {
                Tile neighbour = grid[positionToSearch];

                if (!neighbour.AlreadyExplored && neighbour.IsPath)
                {
                    neighbour.AlreadyExplored = true;
                    neighbour.TileConnectedTo = exploredTile;

                    tilesToExplore.Enqueue(neighbour);
                }
            }
        }
    }

    private void CreatePath()
    {
        Tile tileAddedToPath = grid[endCoordinate];
        while (tileAddedToPath != null)
        {
            pathTiles.Add(tileAddedToPath.transform);
            tileAddedToPath = tileAddedToPath.TileConnectedTo;
        }

        pathTiles.Reverse();
    }
}
