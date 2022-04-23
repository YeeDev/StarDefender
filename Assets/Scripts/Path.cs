using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Path
{
    [SerializeField] Tile startTile = null;
    [SerializeField] Tile endTile = null;

    List<Tile> path;

    public Path() { path = new List<Tile>(); }

    public List<Tile> GetPath { get => path; }
    public List<Tile> SetPath { set => path = value; }
    public Tile StartTile { get => startTile; }
    public Tile EndTile { get => endTile; }
}
