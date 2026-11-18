using UnityEngine;

using UnityEngine.InputSystem;



public class SpawnManager : MonoBehaviour

{

    public GameObject objectPrefab;

    public Transform[] spawnPoints;

    public float accelerationRate = 0.05f;
    public float maxTimeScale = 3.0f;

    public void SpawnObject()

    {

        if (objectPrefab == null)
        {
            Debug.Log("No prefab assigned.");
            return;
        }



        if (spawnPoints.Length == 0)
        {
            Debug.Log("No spawn points assigned.");
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);

        Transform selectedSpawnPoint = spawnPoints[randomIndex];

        GameObject obj = Instantiate(
            objectPrefab,
            selectedSpawnPoint.position,
            selectedSpawnPoint.rotation * Quaternion.Euler(0, 180, 0)
        );
    }

}