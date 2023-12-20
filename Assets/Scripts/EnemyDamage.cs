using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int maxHits = 10; // Set the maximum number of hits required to make the enemy disappear
    private HashSet<GameObject> hitBullets = new HashSet<GameObject>();
    private int currentHits = 0;

    private void OnTriggerEnter(Collider other)
    {
        HandleHitObject(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        HandleHitObject(collision.gameObject);
    }

    private void HandleHitObject(GameObject obj)
    {
        // Check if the object is a bullet (you can customize the tag or layer)
        if (obj.CompareTag("Bullet") && !hitBullets.Contains(obj))
        {
            // Add the bullet to the set to avoid double counting
            hitBullets.Add(obj);

            // Increase the hit count
            currentHits++;

            Destroy(obj);

            // Check if the maximum hits have been reached
            if (currentHits >= maxHits)
            {
                // Call the function to make the enemy disappear
                DestroyEnemy();
            }
        }
    }

    private void DestroyEnemy()
    {
        // Add any additional effects or logic before destroying the enemy, if needed
        // For example, you might want to play an explosion animation or sound.

        // Destroy the enemy GameObject
        Destroy(gameObject);
    }
}