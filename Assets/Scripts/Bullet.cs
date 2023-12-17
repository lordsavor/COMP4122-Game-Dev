using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletGravityScale = 0.2f; // Adjust this value to change the gravity scale

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true; // Enable gravity for the bullet

        // Destroy the bullet and its GameObject after 5 seconds
        Destroy(gameObject, 5f);
    }

    void FixedUpdate()
    {
        // Modify gravity scale indirectly by adjusting Physics.gravity
        Physics.gravity = new Vector3(0, -9.81f * bulletGravityScale, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }
}