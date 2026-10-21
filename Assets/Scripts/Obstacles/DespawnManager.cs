using UnityEngine;

public class DespawnManager : MonoBehaviour
{
    public GameManager GameManager;
    void Start()
    {
        // Automatically find the GameManager in the scene at runtime
        GameManager = FindAnyObjectByType<GameManager>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Despawn"))
        {
            Destroy(gameObject);
            //Debug.Log("Obstacle Cleared");

            // GAME MANAGER INCREASE SCORE HERE
            GameManager.AddScore();
        }

        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            //Debug.Log("Player hit!");

            // GAME MANAGER DECREASE LIFE HERE
            GameManager.LoseAttempt();
        }
    }
}
