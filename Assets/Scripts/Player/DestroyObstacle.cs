using UnityEditor.Build.Content;
using UnityEngine;

public class DestroyObstacle : MonoBehaviour
{
    public SpawnManager spawnManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);
            Debug.Log("Player hit!");
        }
    }
}
