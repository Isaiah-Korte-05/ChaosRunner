using UnityEngine;

[System.Serializable]
public class ObstacleType
{
    public string name = "Obstacle";
    public GameObject prefab;
    [Min(0f)] public float weight = 1f;  // 0 = never spawns
    public int minScore = 0;             // unlocks once playerScore reaches this
    public float yOffset = 0f;           // raises tall obstacles so they sit on the ground
}

public class SpawnManager : MonoBehaviour
{
    public ObstacleType[] obstacleTypes;
    public GameObject shieldPrefab;
    [Range(0f, 1f)] public float shieldChance = 0.08f;

    public Transform[] spawnPoints;

    public float accelerationRate = 0.05f;
    public float maxTimeScale = 3.0f;

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    public void SpawnObject()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.Log("No spawn points assigned.");
            return;
        }

        Transform selectedSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject prefabToSpawn;
        float yOffset = 0f;

        if (shieldPrefab != null && Random.value < shieldChance)
        {
            prefabToSpawn = shieldPrefab;
        }
        else
        {
            ObstacleType type = PickObstacleType();
            if (type == null)
            {
                Debug.Log("No obstacle types available.");
                return;
            }
            prefabToSpawn = type.prefab;
            yOffset = type.yOffset;
        }

        Instantiate(
            prefabToSpawn,
            selectedSpawnPoint.position + Vector3.up * yOffset,
            selectedSpawnPoint.rotation * Quaternion.Euler(0, 180, 0)
        );
    }

    ObstacleType PickObstacleType()
    {
        int score = gameManager != null ? gameManager.playerScore : 0;

        float totalWeight = 0f;
        foreach (ObstacleType t in obstacleTypes)
            if (t.prefab != null && score >= t.minScore) totalWeight += t.weight;

        if (totalWeight <= 0f) return null;

        float roll = Random.value * totalWeight;
        ObstacleType last = null;
        foreach (ObstacleType t in obstacleTypes)
        {
            if (t.prefab == null || score < t.minScore || t.weight <= 0f) continue;
            last = t;
            roll -= t.weight;
            if (roll <= 0f) return t;
        }
        return last;
    }
}