using UnityEngine;

public class SineSway : MonoBehaviour
{
    public float laneSpacing = 3f;
    public float frequency = 1.5f;

    private float startX;
    private float targetX;
    private float spawnTime;

    void Start()
    {
        startX = transform.position.x;
        spawnTime = Time.time;

        // Center-lane obstacles drift to a random side lane; edge-lane ones drift to the center
        if (Mathf.Abs(startX) < 0.1f)
            targetX = Random.value < 0.5f ? -laneSpacing : laneSpacing;
        else
            targetX = 0f;
    }

    void Update()
    {
        float t = (1f - Mathf.Cos((Time.time - spawnTime) * frequency)) * 0.5f;
        Vector3 p = transform.position;
        p.x = Mathf.Lerp(startX, targetX, t);
        transform.position = p;
    }
}