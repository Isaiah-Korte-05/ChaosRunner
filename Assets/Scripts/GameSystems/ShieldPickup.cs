using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.ActivateShield();
            Destroy(gameObject);
        }
        else if (other.CompareTag("Despawn"))
        {
            // Missed it, clean up without scoring
            Destroy(gameObject);
        }
    }
}