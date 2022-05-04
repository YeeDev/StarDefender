using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Wave")]
public class Wave : ScriptableObject
{
    [SerializeField] [Range(1, 50)] int numberOfEnemies = 1;
    [SerializeField] [Range(1, 10)] float spawnRate = 1;
    [SerializeField] Vector2Int spawnCoordinates = Vector2Int.zero;
    [SerializeField] Vector2Int firstTileCoordinates = Vector2Int.zero;
    [SerializeField] Vector2Int endTileCoordinates = Vector2Int.zero;
    [SerializeField] GameObject enemyType = null;

    public int NumberOfEnemies { get => numberOfEnemies; }
    public float SpawnRate { get => spawnRate; }
    public Vector2Int SpawnCoordinates { get => spawnCoordinates; }
    public Vector2Int StartCoordinates { get => firstTileCoordinates; }
    public Vector2Int GoalCoordinates { get => endTileCoordinates; }
    public GameObject EnemyType { get => enemyType; }
}
