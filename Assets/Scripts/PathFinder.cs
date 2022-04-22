using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Tile startTile = null;
    [SerializeField] Tile endTile = null;
    [Tooltip("Unity's Grid Snap Settings")]
    [SerializeField] int unityGridSize = 1;

    List<Tile> path = new List<Tile>();
    Vector2Int[] directions = { Vector2Int.right, Vector2Int.up, Vector2Int.down, Vector2Int.left };
    Dictionary<Vector2Int, Tile> tiles = new Dictionary<Vector2Int, Tile>();

    public List<Tile> GetPath { get => path; }

    private void Awake()
    {
        CreateTilesDictionary();
        SetTileConnections(startTile);
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

    //Creates the Flow Field
    private void SetTileConnections(Tile tileToSearch)
    {
        tileToSearch.HasBeenExplored = true;

        foreach (Vector2Int direction in directions)
        {
            Vector2Int coordinateToCheck = tileToSearch.Coordinates + direction;

            if (tiles.ContainsKey(coordinateToCheck) && !tiles[coordinateToCheck].HasBeenExplored)
            {
                tileToSearch.ConnectedTo = tiles[coordinateToCheck];
                tileToSearch = tiles[coordinateToCheck];
                break;
            }
        }

        if (tileToSearch == endTile) { return; }

        SetTileConnections(tileToSearch);
    }

    private void CreatePath()
    {
        path = new List<Tile>();
        Tile addedTile = startTile;

        while (addedTile != endTile)
        {
            path.Add(addedTile);
            addedTile = addedTile.ConnectedTo;
        }

        path.Add(endTile);
    }
}
