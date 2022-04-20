using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class CoodinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.grey;
    [SerializeField] Color exploredColor = new Color(1f, 0.5f, 0f);
    [SerializeField] Color pathColor = Color.blue;

    Vector2Int coordinates = new Vector2Int();
    TextMeshPro label;
    PathFinder pathFinder;

    private void Awake()
    {
        pathFinder = FindObjectOfType<PathFinder>();
        label = GetComponentInChildren<TextMeshPro>();
        label.enabled = false;

        CalculateCoordinate();
        DisplayCoordinates();
    }

    private void Update()
    {
        if (!Application.isPlaying)
        {
            CalculateCoordinate();
            DisplayCoordinates();
        }

        ToggleLabels();
        SetLabelColor();
    }

    private void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C)) { label.enabled = !label.IsActive(); }
    }

    private void CalculateCoordinate()
    {
        coordinates.x = Mathf.RoundToInt(transform.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.position.z / UnityEditor.EditorSnapSettings.move.z);
    }

    private void DisplayCoordinates()
    {
        label.text = coordinates.ToString();
        transform.name = coordinates.ToString();
    }

    private void SetLabelColor()
    {
        if (pathFinder == null) { return; }

        Node node = pathFinder.GetNode(coordinates);

        if (node == null) { return; }

        if (node.isWalkable) { label.color = blockedColor; }
        else if (node.isPath) { label.color = pathColor; }
        else if (node.isExplored) { label.color = exploredColor; }
        else { label.color = defaultColor; }
    }
}
