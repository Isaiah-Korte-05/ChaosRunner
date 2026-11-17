using UnityEngine;

public class LockCameraY : MonoBehaviour
{
    public Transform player;
    public float lockedY = 2f;

    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = new Vector3(player.position.x, lockedY, -15f);
        }
    }
}