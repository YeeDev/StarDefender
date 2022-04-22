using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Tile startTile = null;
    [SerializeField] Tile endTile = null;
    [Tooltip("Unity's Grid Snap Settings")]
    [SerializeField] int unityGridSize = 1;

    Tile tileToExplore;
    Queue<Tile> tilesToExplore = new Queue<Tile>();
    List<Tile> path = new List<Tile>();
    Vector2Int[] directions = { Vector2Int.right, Vector2Int.up, Vector2Int.down, Vector2Int.left };
    Dictionary<Vector2Int, Tile> tiles = new Dictionary<Vector2Int, Tile>();

    public List<Tile> GetPath { get => path; }

    private void Awake()
    {
        tilesToExplore.Enqueue(startTile);
        CreateTilesDictionary();
        BreathFirstSearch();
        CreatePath();
    }

    private void CreateTilesDictionary()
    {
        foreach (Transform child in transform)
        {
            Tile tile = child.GetComponent<Tile>();

            if (tile != null && tile.IsPath)
            {
                tile.Coordinates = GetCoordinatesFromPosition(child.position);
                tiles.Add(tile.Coordinates, tile);
            }
        }
    }

    private Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x / unityGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / unityGridSize);

        return coordinates;
    }

    private void BreathFirstSearch()
    {
        bool searching = true;
        while (searching)
        {
            tileToExplore = tilesToExplore.Dequeue();
            tileToExplore.HasBeenExplored = true;
            ExploreNeighbors();

            if (tileToExplore == endTile)
            {
                searching = false;
            }
        }
    }

    private void ExploreNeighbors()
    {
        foreach (Vector2Int direction in directions)
        {
            Vector2Int coordinateToCheck = tileToExplore.Coordinates + direction;

            if (tiles.ContainsKey(coordinateToCheck) && !tiles[coordinateToCheck].HasBeenExplored)
            {
                Tile neighbor = tiles[coordinateToCheck];
                neighbor.ConnectedTo = tileToExplore;
                neighbor.HasBeenExplored = true;
                tilesToExplore.Enqueue(neighbor);
            }
        }
    }

    private void CreatePath()
    {
        Tile currentTile = endTile;

        path.Add(currentTile);

        while (currentTile.ConnectedTo != null)
        {
            currentTile = currentTile.ConnectedTo;
            path.Add(currentTile);
        }

        path.Reverse();
    }
}
