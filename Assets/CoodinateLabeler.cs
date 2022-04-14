using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class CoodinateLabeler : MonoBehaviour
{
    Vector2Int coordinates = new Vector2Int();
    TextMeshPro label;

    private void Awake()
    {
        label = GetComponentInChildren<TextMeshPro>();
        DisplayCoordinates();
    }

    private void Update()
    {
        if (!Application.isPlaying)
        {
            CalculateCoordinate();
            DisplayCoordinates();
        }
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
