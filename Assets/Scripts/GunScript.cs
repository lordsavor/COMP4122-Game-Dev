using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletBaseSpeed = 2000;
    public float roundsPerMinute = 600;
     public float bulletGravityScale = 0.5f;
    
    private float timeBetweenShots;
    private bool canFire = true;

    void Start()
    {
        timeBetweenShots = 60f / roundsPerMinute;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && canFire)
        {
            StartCoroutine(FireBullets());
        }
    }

    IEnumerator FireBullets()
    {
        canFire = false;

        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        
        // Scale the bullet speed with the plane speed
        float planeSpeed = GetComponentInParent<Rigidbody>().velocity.magnitude;
        float scaledBulletSpeed = bulletBaseSpeed + planeSpeed; // Adjust this scaling factor as needed

        // Print the plane speed to the console
        Debug.Log("Plane Speed: " + planeSpeed);
        
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * scaledBulletSpeed;

        // Destroy the bullet after 5 seconds (adjust as needed)
        Destroy(bullet, 5f);

        yield return new WaitForSeconds(timeBetweenShots);

        canFire = true;
    }
}
