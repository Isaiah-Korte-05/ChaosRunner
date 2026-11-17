using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public GameObject[] chunkPrefabs;
    public float moveSpeed = 5f;
    public float chunkLength = 40f;
    public int visibleChunks = 5;

    private List<Transform> activeChunks = new List<Transform>();

    void Start()
    {
        for (int i = 0; i < visibleChunks; i++)
        {
            SpawnChunk(i * chunkLength);
        }
    }

    void Update()
    {
        // Move all active chunks backward toward the player
        for (int i = 0; i < activeChunks.Count; i++)
        {
            activeChunks[i].Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.World);

            // Once a chunk is completely behind the camera, recycle it to the front
            if (activeChunks[i].position.z < -chunkLength)
            {
                RecycleChunk(activeChunks[i]);
            }
        }
    }

    void SpawnChunk(float zPosition)
    {
        int randomIndex = Random.Range(0, chunkPrefabs.Length);
        GameObject newChunk = Instantiate(chunkPrefabs[randomIndex], new Vector3(0, 0, zPosition), Quaternion.identity);
        activeChunks.Add(newChunk.transform);
    }

    void RecycleChunk(Transform chunk)
    {
        // Find the furthest Z position among all current chunks
        float furthestZ = 0f;
        foreach (Transform c in activeChunks)
        {
            if (c.position.z > furthestZ)
                furthestZ = c.position.z;
        }

        // Move this chunk directly to the end of the line
        chunk.position = new Vector3(0, 0, furthestZ + chunkLength);
    }
}