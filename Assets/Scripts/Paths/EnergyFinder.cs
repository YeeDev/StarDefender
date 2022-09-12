using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarDef.Tiles;

namespace StarDef.Paths
{
    public class EnergyFinder : MonoBehaviour
    {
        Vector2Int[] directions = { Vector2Int.right, Vector2Int.up, Vector2Int.down, Vector2Int.left };

        Queue<Tile> tilesToExplore = new Queue<Tile>();
        Dictionary<Vector2Int, Tile> grid = new Dictionary<Vector2Int, Tile>();

        private void Awake() { LoadTiles(); }

        private void LoadTiles()
        {
            var waypoints = FindObjectsOfType<Tile>();
            foreach (Tile tile in waypoints)
            {
                if (!tile.IsEnergy && !tile.IsGenerator) { continue; }

                var gridPos = tile.GridCoordinates;
                if (grid.ContainsKey(gridPos)) { Debug.LogWarning("Skipping overlapping block " + tile); }
                else { grid.Add(gridPos, tile); }
            }
        }

        public List<MeshRenderer> FindEnergy(Vector2Int startCoordinate)
        {
            ClearFlow();

            tilesToExplore.Enqueue(grid[startCoordinate]);
            Vector2Int endCoordinate = Vector2Int.zero;

            while (tilesToExplore.Count > 0)
            {
                Tile exploringTile = tilesToExplore.Dequeue();

                ExploreNeighbours(exploringTile);

                if (exploringTile.IsGenerator)
                {
                    endCoordinate = exploringTile.GridCoordinates;
                    Debug.Log("running") ;
                    tilesToExplore.Clear();
                }
            }

            return CreatePath(endCoordinate);
        }

        private void ClearFlow()
        {
            foreach (var tile in grid)
            {
                grid[tile.Key].TileConnectedTo = null;
                grid[tile.Key].AlreadyExplored = false;
            }

            tilesToExplore.Clear();
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

                    if (!neighbour.AlreadyExplored && neighbour.IsEnergy)
                    {
                        neighbour.AlreadyExplored = true;
                        neighbour.TileConnectedTo = exploredTile;

                        tilesToExplore.Enqueue(neighbour);
                    }
                }
            }
        }

        private List<MeshRenderer> CreatePath(Vector2Int endCoordinate)
        {
            List<MeshRenderer> path = new List<MeshRenderer>();

            Tile tileAddedToPath = grid[endCoordinate];
            while (tileAddedToPath != null)
            {
                path.Add(tileAddedToPath.Indicator);
                tileAddedToPath = tileAddedToPath.TileConnectedTo;
            }

            return path;
        }
    }
}
