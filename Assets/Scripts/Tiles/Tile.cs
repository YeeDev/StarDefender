using UnityEngine;

namespace StarDef.Tiles
{
    [ExecuteInEditMode]
    public class Tile : MonoBehaviour
    {
        [Tooltip("Unity's Grid Snap Settings")]
        [SerializeField] int unityGridSize = 2;
        [SerializeField] bool isPath, isEnergy, isGenerator = false;
        [SerializeField] MeshRenderer energyIndicator = null;

        bool alreadyExplored;
        Tile connectedTo = null;
        TextMesh text;

        public bool IsPath { get => isPath; }
        public bool IsEnergy { get => isEnergy; }
        public bool IsGenerator { get => isGenerator; }
        public bool AlreadyExplored { get => alreadyExplored; set => alreadyExplored = value; }
        public Tile TileConnectedTo { get => connectedTo; set => connectedTo = value; }
        public MeshRenderer Indicator { get => energyIndicator; }
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

            if (Application.isPlaying) { text.gameObject.SetActive(false); }
        }

        void Update()
        {
            if (Application.isPlaying) { return; }

            Vector2Int coordinates = GridCoordinates;

            text.text = $"({coordinates.x},{coordinates.y})";
            transform.name = coordinates.ToString();
        }
    }
}