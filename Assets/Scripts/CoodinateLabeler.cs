using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class CoodinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.grey;

    Vector2Int coordinates = new Vector2Int();
    TextMeshPro label;
    Waypoint waypoint;

    private void Awake()
    {
        label = GetComponentInChildren<TextMeshPro>();
        label.enabled = false;

        waypoint = GetComponent<Waypoint>();
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
}
