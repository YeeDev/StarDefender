using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [Tooltip("Unity's Grid Snap Settings")]
    [SerializeField] int unityGridSize = 1;

    Vector2Int[] directions = { Vector2Int.right, Vector2Int.up, Vector2Int.down, Vector2Int.left };

    Queue<Tile> tilesToExplore = new Queue<Tile>();
    Dictionary<Vector2Int, Tile> tiles = new Dictionary<Vector2Int, Tile>();

    private void Awake() { CreateTilesDictionary(); }

    //Creates a reference of all tiles using their coordinates.
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

    //Creates a path based on a starting tile and an endtile.
    public List<Tile> CreatePath(Tile startTile, Tile endTile)
    {
        ClearFlow();

        tilesToExplore.Enqueue(startTile);

        BreathFirstSearch(endTile);

        return GetPath(endTile);
    }

    //Clears all connections before looking for a new path.
    private void ClearFlow()
    {
        foreach (var item in tiles)
        {
            tiles[item.Key].ConnectedTo = null;
        }

        tilesToExplore.Clear();
    }

    private void BreathFirstSearch(Tile endTile)
    {
        bool searching = true;
        List<Tile> tilesInQueue = new List<Tile>();

        while (searching)
        {
            Tile tileToExplore = tilesToExplore.Dequeue();
            tilesInQueue.Add(tileToExplore);
            ExploreNeighbors(tilesInQueue, tileToExplore);

            if (tileToExplore == endTile)
            {
                searching = false;
            }
        }
    }

    private void ExploreNeighbors(List<Tile> tilesInQueue, Tile tileToExplore)
    {
        foreach (Vector2Int direction in directions)
        {
            Vector2Int coordinateToCheck = tileToExplore.Coordinates + direction;
            Tile neighbor = tiles.ContainsKey(coordinateToCheck) ? tiles[coordinateToCheck] : null;

            if (neighbor != null && !tilesInQueue.Contains(neighbor))
            {
                neighbor.ConnectedTo = tileToExplore;
                tilesToExplore.Enqueue(neighbor);
                tilesInQueue.Add(neighbor);
            }
        }
    }

    private List<Tile> GetPath(Tile endTile)
    {
        List<Tile> path = new List<Tile>();
        Tile currentTile = endTile;

        path.Add(currentTile);

        while (currentTile.ConnectedTo != null)
        {
            currentTile = currentTile.ConnectedTo;
            path.Add(currentTile);
        }

        path.Reverse();
        return path;
    }
}
