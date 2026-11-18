using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float launchForce = 18f;
    public float fireCooldown = 0.5f;
    private float nextShotTime;

    public GameManager GameManager;
    void Start()
    {
        // Automatically find the GameManager in the scene at runtime
        GameManager = FindAnyObjectByType<GameManager>();
    }

    void Update()
    {
        if(Keyboard.current != null && Keyboard.current.wKey.wasPressedThisFrame
            && Time.time >= nextShotTime && GameManager.CanShoot()) 
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject obj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = obj.GetComponent<Rigidbody>();

        if(rb == null)
        {
            Debug.LogError("Projectile prefab needs RigidBody and ProjectileImpact");
            Destroy(obj);
            return;
        }

        GameManager.shotFired();
        rb.AddForce(firePoint.forward * launchForce, ForceMode.Impulse);
        Destroy(obj, 1f);
        nextShotTime = Time.time + fireCooldown;
    }
}   
