using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Tile : MonoBehaviour
{
    [Tooltip("Unity's Grid Snap Settings")]
    [SerializeField] int unityGridSize = 2;
    [SerializeField] bool isPath = false;

    bool alreadyExplored;
    TextMesh text;

    public bool IsPath { get => isPath; }
    public bool AlreadyExplored { get => alreadyExplored; set => alreadyExplored = value; }
    public Vector2Int GridCoordinates
    {
        get
        {
            Vector2Int coordinates = new Vector2Int();
            coordinates.x = Mathf.RoundToInt(transform.position.x / unityGridSize);
            coordinates.y = Mathf.RoundToInt(transform.position.z / unityGridSize);

            return coordinates;
        }
    }


    private void Awake()
    {
        text = GetComponentInChildren<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2Int coordinates = GridCoordinates;

        text.text = $"({coordinates.x},{coordinates.y})";
        transform.name = coordinates.ToString();
    }
}
