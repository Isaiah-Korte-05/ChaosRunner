using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    // fixed speed, but Time.deltaTime is slowly increased during the game
    public float speed = 5f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
