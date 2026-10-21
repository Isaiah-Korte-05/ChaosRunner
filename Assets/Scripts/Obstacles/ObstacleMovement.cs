using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float minSpeed = 3f;
    public float maxSpeed = 8f;

    public float speed = 5f;

    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the "Player" tag
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collided with player!");
        }
    }
}
