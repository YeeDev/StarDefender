using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Tile : MonoBehaviour
{
    [Tooltip("Unity's Grid Snap Settings")]
    [SerializeField] int unityGridSize = 2;

    TextMesh text;

    private void Awake()
    {
        text = GetComponentInChildren<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(transform.position.x / unityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.position.z / unityGridSize);

        text.text = $"({coordinates.x},{coordinates.y})";
    }
}
