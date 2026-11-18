using System.Threading;
using UnityEngine;

public class ProjectileImpact : MonoBehaviour
{

    public string targetTag = "Obstacle";
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            Destroy(other.gameObject);
        }

        Destroy(gameObject);
    }
}
