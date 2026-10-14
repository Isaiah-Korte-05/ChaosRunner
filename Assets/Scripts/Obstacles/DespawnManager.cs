using UnityEditor.Build.Content;
using UnityEngine;

public class DespawnManager : MonoBehaviour
{
    public SpawnManager spawnManager;
    void OnTriggerEnter(Collider other)
    {
            if (other.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);
            Debug.Log("Obstacle Cleared");
        }
    }
}
