using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    [Tooltip("Unity's Grid Snap Settings")]
    [SerializeField] int unityGridSize;
    [SerializeField] Color pathColor = Color.blue;
    [SerializeField] Color towerColor = Color.green;
    [SerializeField] Color defaultColor = Color.white;

    private void Update()
    {
        foreach (Tile tile in GetComponentsInChildren<Tile>())
        {
            TextMeshPro text = tile.GetComponentInChildren<TextMeshPro>();
            Vector2Int coordinates = GetCoordinatesFromPosition(tile.transform.position);
            string coordinatesToString = $"({coordinates.x}, {coordinates.y})";
            text.text = coordinatesToString;
            tile.transform.name = coordinatesToString;

            SetTextColor(text, tile);
        }
    }

    private Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x / unityGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / unityGridSize);

        return coordinates;
    }


    private void SetTextColor(TextMeshPro text, Tile tile)
    {
        if (tile.IsPath) { text.color = pathColor; }
        else if (tile.IsTower) { text.color = towerColor; }
        else { text.color = defaultColor; }
    }
}
