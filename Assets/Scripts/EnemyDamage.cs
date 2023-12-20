using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int maxHits = 10; // Set the maximum number of hits required to make the enemy disappear
    private int currentHits = 0;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the triggering object is a bullet (you can customize the tag or layer)
        if (other.CompareTag("Bullet"))
        {
            // Increase the hit count
            currentHits++;

            Destroy(other.gameObject);

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