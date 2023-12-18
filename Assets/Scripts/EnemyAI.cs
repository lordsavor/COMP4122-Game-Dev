using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // Moving
    [SerializeField] public int ShipBaseSpeed = 100;

    // Shooting
    [SerializeField] public Transform bulletSpawnPoint;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public float bulletBaseSpeed = 2000;
    [SerializeField] public LayerMask whatIsGround, whatIsPlayer;
    [SerializeField] public Transform Checkpoint;

    // Attacking
    public Transform player;
    private float timeBetweenShots;
    private bool canFire = true;

    // State
    bool attackState = false;
    public float sightRange, attackRange;
    [HideInInspector] public bool playerInSightRange, playerInAttackRange;


    void Start()
    {
        // Look at checkpoint and adjust rotation
        Vector3 targetDirection = (Checkpoint.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up) * Quaternion.Euler(0f, 90f, 0f);
        transform.rotation = targetRotation;

        GetComponent<Rigidbody>().velocity = targetDirection * ShipBaseSpeed;
    }
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (playerInSightRange)
        {
            // Assign the detected player's transform to the player variable
            Collider[] players = Physics.OverlapSphere(transform.position, sightRange, whatIsPlayer);
            if (players.Length > 0)
            {
                player = players[0].transform;
            }
        }

        if (playerInAttackRange)
        {
            attackState = true;
            Attack();
        }
        else
        {
            attackState = false;
        }
    }

    private void Attack()
    {
        if (canFire&& attackState)
        {
            StartCoroutine(FireBullets());
        }
    }

    IEnumerator FireBullets()
    {
        canFire = false;

        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // Get the direction to the target position
        Vector3 targetDirection = (transform.position - bulletSpawnPoint.position).normalized;

        bullet.GetComponent<Rigidbody>().velocity = targetDirection * bulletBaseSpeed;

        // Destroy the bullet after 5 seconds (adjust as needed)
        Destroy(bullet, 5f);

        yield return new WaitForSeconds(timeBetweenShots);

        canFire = true;
    }
}