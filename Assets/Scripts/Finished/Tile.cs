using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower tower = null;
    [SerializeField] bool isCorner = false;
    [SerializeField] bool isPath = false;

    EnergyBank energyBank;
    Vector2Int coordinates;
    Tile connectedTo;

    public bool IsPath { get => isPath; }
    public bool IsCorner { get => isCorner; }
    public bool IsTower { get => tower; }
    public Tile ConnectedTo { get => connectedTo; set => connectedTo = value; }
    public Vector2Int Coordinates { get => coordinates; set => coordinates = value; }

    private void Awake() { energyBank = FindObjectOfType<EnergyBank>(); }
    
    private void OnMouseDown()
    {
        if (tower != null && energyBank.AddActiveTower(tower.ActivateTower()))
        {
            energyBank.CheckIfDeactivateTower();
            energyBank.SetTowerIndicator();
        }
    }
}