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
}
